using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    internal class Simulation : LogicAbstractAPI, INotifyPropertyChanged
    {

        private DataAbstractAPI board;
        private bool running;
        private List<Thread> threads = new List<Thread>();
        private ObservableCollection<IBall> observableData = new ObservableCollection<IBall>();

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

        public override bool isRunning() { return running; }

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
                threads.Clear();
            }
        }

        private void mainLoop()
        {
            Thread tableTask = new Thread(() =>
            {
                lookForCollisions();
                Thread.Sleep(10);
            });
            tableTask.Start();
            foreach (var ball in this.board.getBalls())
            {
                Thread thread = new Thread(() =>
                {
                    while (this.running)
                    {
                        checkBorderCollisionForBall(ball);
                        ball.PropertyChanged += RelayBallUpdate;
                        Thread.Sleep(10);
                    }
                });
                thread.IsBackground = true;
                thread.Start();
                threads.Add(thread);
            }
        }

        private void lookForCollisions()
        {
            while(this.running)
            {
                IBall[] balls = board.getBalls();
                foreach (var ball1 in balls)
                {
                    foreach (var ball2 in balls)
                    {
                        if (ball1 == ball2)
                        { continue; }
                        if (Math.Sqrt((ball1.x - ball2.x) * (ball1.x - ball2.x) + (ball1.y - ball2.y) * (ball1.y - ball2.y)) <= ball1.getSize()/2 + ball2.getSize()/2)
                        {
                            lock (ball1)
                            {
                                lock (ball2)
                                {
                                    ballCollision(ball1, ball2);
                                    checkBorderCollisionForBall(ball1);
                                    ball2.updatePosition();
                                }
                                checkBorderCollisionForBall(ball2);
                                ball1.updatePosition();
                            }
                        }
                    }
                }
            }
        }

        private void ballCollision(IBall ball1, IBall ball2)
        {
            // Oblicz wektor normalny
            float dx = ball2.x - ball1.x;
            float dy = ball2.y - ball1.y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy); // odległość między kulami
            float n_x = dx / distance; // składowa x wektora normalnego
            float n_y = dy / distance; // składowa y wektora normalnego

            // Oblicz wektor styczny
            float t_x = -n_y; // składowa x wektora stycznego
            float t_y = n_x;  // składowa y wektora stycznego

            // Prędkości wzdłuż normalnej i stycznej
            float v1n = ball1.getXVelocity() * (n_x) + ball1.getYVelocity() * (n_y);
            float v1t = ball1.getXVelocity() * (t_x) + ball1.getYVelocity() * (t_y);

            float v2n = ball2.getXVelocity() * (n_x) + ball2.getYVelocity() * (n_y);
            float v2t = ball2.getXVelocity() * (t_x) + ball2.getYVelocity() * (t_y);

            // Nowe prędkości wzdłuż normalnej po zderzeniu
            float u1n = ((ball1.getMass() - ball2.getMass()) * v1n + 2 * ball2.getMass() * v2n) / (ball1.getMass() + ball2.getMass());
            float u2n = ((ball2.getMass() - ball1.getMass()) * v2n + 2 * ball1.getMass() * v1n) / (ball2.getMass() + ball1.getMass());

            // Nowe prędkości całkowite dla każdej kuli
            ball1.setXVelocity(u1n * (n_x) + v1t * (t_x));
            ball1.setYVelocity(u1n * (n_y) + v1t * (t_y));

            ball2.setXVelocity(u2n * (n_x) + v2t * (t_x));
            ball2.setYVelocity(u2n * (n_y) + v2t * (t_y));
            Console.WriteLine("aaa");
        }

        public void checkBorderCollisionForBall(IBall ball)
        {
            if (ball.x + ball.getSize() >= board.sizeX || ball.x + ball.getXVelocity() + ball.getSize() >= board.sizeX ||
                ball.x <= 0 || ball.x + ball.getXVelocity() <= 0)
            {
                //Debug.WriteLine(board.sizeX + ", " + board.sizeY + ", " + ball.x + " , " + ball.y);
                Logic.changeXdirection(ball);
            }
            if (ball.y + ball.getSize() >= board.sizeY || ball.y + ball.getYVelocity() + ball.getSize() >= board.sizeY ||
                ball.y <= 0 || ball.y + ball.getYVelocity() <= 0)
            {
                Logic.changeYdirection(ball);
            }
            updatePosition(ball);
        }

        public override float[][] getCoordinates()
        {
            return board.getCoordinates();
        }

        public override event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        private void RelayBallUpdate(object source, PropertyChangedEventArgs args)
        {
            this.OnPropertyChanged(args);
        }
        public override void getBoardParameters(int x, int y, int ballsAmount)
        {
            board.setBoardParameters(x, y, ballsAmount);
            foreach (var ball in board.getBalls())
            {
                this.observableData.Add((IBall)ball);
            }
        }

        public override void setBalls(IBall[] balls)
        {
            this.board.setBalls(balls);
        }

        public void updatePosition(IBall ball)
        {
            board.updatePosition(ball);

            ball.RaisePropertyChanged(nameof(ball.x));
            ball.RaisePropertyChanged(nameof(ball.y));
        }
    }
}