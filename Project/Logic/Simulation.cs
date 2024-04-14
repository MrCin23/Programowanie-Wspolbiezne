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
    public class Simulation : LogicAbstractAPI, INotifyPropertyChanged
    {

        private Board board;
        private bool running;
        private List<Thread> threads = new List<Thread>();
        private ObservableCollection<DataAbstractAPI> observableData = new ObservableCollection<DataAbstractAPI>();
        public event EventHandler BallsChanged;
        //todo

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
            foreach(var ball in this.board.getBalls())
            {
                Thread thread = new Thread(() =>
                {
                    while (this.running)
                    {
                        this.board.checkBorderCollision();
                        Logic.updatePosition(ball);
                        ball.PropertyChanged += RelayBallUpdate;
                        BallsChanged?.Invoke(this, EventArgs.Empty); // Powiadom o zmianie kulek
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
            //Debug.WriteLine(source.ToString());
            this.OnPropertyChanged(args);
        }
    }
}
/*using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        private ObservableCollection<DataAbstractAPI> observableData = new ObservableCollection<DataAbstractAPI>();

        public override ObservableCollection<DataAbstractAPI> observableDataProperty
        {
            get => observableData;
            set
            {
                observableData = value;
                PropertyChanged += RelayBallUpdate;
            }
        }

        public override Board boardProperty
        {
            get => board;
            set
            {
                board = value;
                PropertyChanged += RelayBallUpdate;
            }
        }

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
                        //ball.PropertyChanged += RelayBallUpdate;
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
            Debug.WriteLine(source.ToString());
            this.OnPropertyChanged(args);
        }
    }
}
*/