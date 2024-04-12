using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class Simulation : LogicAbstractAPI, INotifyPropertyChanged
    {

        private Board board;
        private bool running;
        private List<Thread> threads = new List<Thread>();
        private ObservableCollection<DataAbstractAPI> observableData;
        

        public Simulation(Board board) 
        { 
            this.board = board;
            this.running = false;
            foreach (var ball in  board.getBalls())
            {
                this.observableData.Add(ball);
            }
        }

        public override Board getBoard()
        {
            return board;
        }

        public override ObservableCollection<DataAbstractAPI> getObservableData() { return observableData; }

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
                foreach (var thread in threads)
                {
                    thread.Interrupt();
                }
                threads.Clear();
            }
        }

        private void mainLoop()
        {
            foreach(var ball in board.getBalls())
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        private void RelayBallUpdate(object source, PropertyChangedEventArgs args)
        {
            this.OnPropertyChanged(args);
        }
    }
}
