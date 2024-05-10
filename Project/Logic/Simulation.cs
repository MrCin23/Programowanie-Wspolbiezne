using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        private List<Task> tasks = new List<Task>();
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
                tasks.Clear();
            }
        }

        private void mainLoop()
        {
            /*Task tableTask = new Task(() => board.lookForCollisions());
            tableTask.Start();*/
            foreach (var ball in this.board.getBalls())
            {
                Task task = new Task(() =>
                {
                    while (this.running)
                    {
                        checkBorderCollisionForBall(ball);
                        //Logic.updatePosition(ball);
                        ball.PropertyChanged += RelayBallUpdate;
                        //Thread.Sleep(10); //Przemyśleć jak to zmienić
                        Task.Delay(10);
                    }
                });
//                task.IsBackground = true;
                task.Start();
                tasks.Add(task);
            }
        }

        public void checkBorderCollision()
        {

            foreach (var ball in board.getBalls())
            {
                if (ball.x + ball.getSize() >= board.sizeX || ball.x + ball.getXVelocity() + ball.getSize() >= board.sizeX ||
                    ball.x <= 0 || ball.x + ball.getXVelocity() <= 0)
                {
                    Logic.changeXdirection(ball);
                    updatePosition(ball);
                }
                if (ball.y + ball.getSize() >= board.sizeY || ball.y + ball.getYVelocity() + ball.getSize() >= board.sizeY ||
                    ball.y <= 0 || ball.y + ball.getYVelocity() <= 0)
                {
                    Logic.changeYdirection(ball);
                    updatePosition(ball);
                }
            }
        }

        public void checkBorderCollisionForBall(IBall ball)
        {
            if (ball.x + ball.getSize() >= board.sizeX || ball.x + ball.getXVelocity() + ball.getSize() >= board.sizeX ||
                ball.x <= 0 || ball.x + ball.getXVelocity() <= 0)
            {
                Debug.WriteLine(board.sizeX + ", " + board.sizeY + ", " + ball.x + " , " + ball.y);
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