using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    internal class Simulation : LogicAbstractAPI
    {
        private DataAbstractAPI board;
        private bool running { get; set; }
        private Thread collisionThread;
        private IBall[] balls;
        private ObservableCollection<IBall> observableData = new ObservableCollection<IBall>();
        public readonly object lockk = new object();

        public Simulation(DataAbstractAPI board = null)
        {
            if (board == null)
            {
                this.board = DataAbstractAPI.CreateDataAPI();
            }
            else
            {
                this.board = board;
            }
            this.running = false;
        }

        public void setBoard(IBoard board)
        {
            this.board = (DataAbstractAPI)board;
        }

        internal override DataAbstractAPI getBoard()
        {
            return board;
        }

        public override IBall[] getBalls()
        {
            return getBoard().getBalls();
        }

        public override void startSimulation()
        {
            if(!running)
            {
                this.running = true;
                mainLoop();
            }
        }

        public override void stopSimulation() 
        { 
            if(running)
            {
                this.running = false;
                this.collisionThread.Interrupt();
                foreach(IBall b in balls)
                {
                    b.destroy();
                }
            }
        }

        private void mainLoop()
        {
            collisionThread = new Thread(() =>
            {
                try
                {
                    while (running)
                    {
                        lookForCollisions();
                        Thread.Sleep(5);
                    }
                }
                catch (ThreadInterruptedException) 
                {
                    Debug.WriteLine("Thread killed");
                }
            });
            collisionThread.IsBackground = true;
            collisionThread.Start();
        }

        private void lookForCollisions()
        {
            foreach (IBall ball1 in balls)
            {
                lock(lockk)
                {
                    checkBorderCollisionForBall(ball1);
                    foreach (IBall ball2 in balls)
                    {
                        if (ball1 == ball2)
                        { continue; }
                        Vector2 tmp1 = ball1.pos;
                        Vector2 tmp2 = ball2.pos;
                        if (Math.Sqrt((tmp1.X - tmp2.X) * (tmp1.X - tmp2.X) + (tmp1.Y - tmp2.Y) * (tmp1.Y - tmp2.Y)) <= ball1.getSize() / 2 + ball2.getSize() / 2)
                        {
                            ballCollision(ball1, ball2);
                        }
                    }
                }
            }
        }

        private void ballCollision(IBall ball1, IBall ball2)
        {
            // Oblicz wektor normalny
            float dx = ball2.pos.X - ball1.pos.X;
            float dy = ball2.pos.Y - ball1.pos.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy); // odległość między kulami
            float n_x = dx / distance; // składowa x wektora normalnego
            float n_y = dy / distance; // składowa y wektora normalnego

            // Oblicz wektor styczny
            float t_x = -n_y; // składowa x wektora stycznego
            float t_y = n_x;  // składowa y wektora stycznego

            // Prędkości wzdłuż normalnej i stycznej
            float v1n = ball1.vel.X * (n_x) + ball1.vel.Y * (n_y);
            float v1t = ball1.vel.X * (t_x) + ball1.vel.Y * (t_y);

            float v2n = ball2.vel.X * (n_x) + ball2.vel.Y * (n_y);
            float v2t = ball2.vel.X * (t_x) + ball2.vel.Y * (t_y);

            // Nowe prędkości wzdłuż normalnej po zderzeniu
            float u1n = ((ball1.getMass() - ball2.getMass()) * v1n + 2 * ball2.getMass() * v2n) / (ball1.getMass() + ball2.getMass());
            float u2n = ((ball2.getMass() - ball1.getMass()) * v2n + 2 * ball1.getMass() * v1n) / (ball2.getMass() + ball1.getMass());

            // Nowe prędkości całkowite dla każdej kuli
            Vector2 vel1 = new Vector2(u1n * n_x + v1t * t_x, u1n * n_y + v1t * t_y);
            Vector2 vel2 = new Vector2(u2n * n_x + v2t * t_x, u2n * n_y + v2t * t_y);
            lock (lockk)
            {
                ball1.vel = vel1;
                ball2.vel = vel2;
            }
        }

        public void checkBorderCollisionForBall(IBall ball)
        {
            lock(lockk)
            {
                if (ball.pos.X + ball.getSize() >= board.sizeX || ball.pos.X + ball.vel.X + ball.getSize() >= board.sizeX ||
                    ball.pos.X <= 0 || ball.pos.X + ball.vel.X <= 0)
                {
                    Logic.changeXdirection(ball);
                }
                if (ball.pos.Y + ball.getSize() >= board.sizeY || ball.pos.Y + ball.vel.Y + ball.getSize() >= board.sizeY ||
                    ball.pos.Y <= 0 || ball.pos.Y + ball.vel.Y <= 0)
                {
                    Logic.changeYdirection(ball);
                }
            }
        }

        public override Vector2[] getCoordinates()
        {
            return board.getCoordinates();
        }

        #nullable enable
        public override event EventHandler<LogicEventArgs>? ChangedPosition;

        private void OnPropertyChanged(LogicEventArgs args)
        {
            ChangedPosition?.Invoke(this, args);
        }
        public override void getBoardParameters(int x, int y, int ballsAmount)
        {
            board.setBoardParameters(x, y, ballsAmount);
            foreach (IBall ball in board.getBalls())
            {
                this.observableData.Add(ball);
                ball.ChangedPosition += sendUpdate;
            }
            this.balls = board.getBalls();
        }

        public override void setBalls(IBall[] balls)
        {
            this.board.setBalls(balls);
        }

        private void sendUpdate(object sender, DataEventArgs e)
        {
            IBall ball  = (IBall)sender;
            Vector2 pos = ball.pos;
            LogicEventArgs args = new LogicEventArgs(pos);
            OnPropertyChanged(args);
        }
    }
}