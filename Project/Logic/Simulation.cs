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

        private Board board;
        private bool running;
        private List<Thread> threads = new List<Thread>();
        private ObservableCollection<DataAbstractAPI> observableData = new ObservableCollection<DataAbstractAPI>();

        public Simulation(Board board = null)
        {
            if (board == null)
            {
                this.board = CreateBoard();
            }
            else
            {
                this.board = board;
            }
            this.running = false;
        }

        public void setBoard(IBoard board)
        {
            this.board = (Board)board;
        }

        internal override Board getBoard()
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
            }
            mainLoop();
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
            foreach (var ball in this.board.getBalls())
            {
                Thread thread = new Thread(() =>
                {
                    while (this.running)
                    {
                        this.board.checkBorderCollision();
                        Logic.updatePosition(ball);
                        ball.PropertyChanged += RelayBallUpdate;
                        Thread.Sleep(10);
                    }
                });
                thread.IsBackground = true;
                thread.Start();
                threads.Add(thread);
            }
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
                this.observableData.Add((DataAbstractAPI)ball);
            }
        }

        public override void setBalls(IBall[] balls)
        {
            this.board.balls = balls;
        }
    }
}