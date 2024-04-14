using Data;
using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Model
{
    /*    public abstract class ModelAbstractAPI 
        {
            public static ModelAbstractAPI CreateModelAPI(int x, int y, int amount)
            {
                //return null;
                return new Model(LogicAbstractAPI.CreateLogicAPI(x, y, amount));
            } 
            public abstract void startSimulation();
            public abstract void stopSimulation();
            public abstract float[][] getCoordinates();
        }*/

    public abstract class ModelAbstractAPI : IObservable<IBall>
    {
        public static ModelAbstractAPI CreateModelAPI(int amount)
        {
            Model model = new Model(LogicAbstractAPI.CreateLogicAPI(700, 300, amount), amount);
            //Model model = new Model();
            return model;

        }

        public abstract void StartSimulation();
        public abstract void StopSimulation();
        public abstract IDisposable Subscribe(IObserver<IBall> observer);

        public abstract DrawBalls[] getballs();

        public abstract event PropertyChangedEventHandler PropertyChanged;


    }


    internal class Model : ModelAbstractAPI
    {
        private IObservable<EventPattern<BallChangeEventArgs>> eventObservable = null;
        public LogicAbstractAPI simulation { get; set; }
        public override event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<BallChangeEventArgs> BallChanged;
        public IObservable<EventHandler> ballsChanged;
        //public ObservableCollection<BallToDraw> drawBalls;
        DrawBalls[] drawBalls;

        //do usuniecia
        public override DrawBalls[] getballs()
        {
            return drawBalls;
        }
        private int amount { get; set; }


        public Model(LogicAbstractAPI api, int amount)
        {
            eventObservable = Observable.FromEventPattern<BallChangeEventArgs>(this, "BallChanged");
            simulation = api;
            drawBalls = new DrawBalls[amount];
            for (int i = 0; i < amount; i++)
            {
                DrawBalls ball = new DrawBalls(api.getCoordinates()[i][0], api.getCoordinates()[i][1]);
                //Debug.WriteLine(ball.x + " " + ball.y);
                drawBalls[i] = ball;
                /*Thread thread = new Thread(() =>
                {
                    while (true)
                    {*/
                api.PropertyChanged += OnBallChanged;
                    /*}
                });
                thread.IsBackground = true;
                thread.Start();*/
            }
            //simulation.BallsChanged += (sender, args) => UpdateDrawBallsPositions();

            /*            db = new DrawBalls(simulation);
                        drawBalls = db.ballsToDraw;
                        //Debug.WriteLine();
                        foreach (var ball in drawBalls)
                        {
                            Debug.WriteLine(ball.X + " " + ball.Y);
                        }*/
        }
        private void OnBallChanged(object sender, PropertyChangedEventArgs args)
        {        
            if (drawBalls[0].x != simulation.getCoordinates()[0][0] && drawBalls[0].y != simulation.getCoordinates()[0][1])
            {
                UpdatePosition();
                //Debug.WriteLine(simulation.getCoordinates()[0][0] + "  " + simulation.getCoordinates()[0][1]);
            }
        }
        private void UpdatePosition()
        {
            // Tutaj implementuj logikę aktualizacji pozycji kulek DrawBalls na podstawie danych z symulacji
            // Przykładowo:
            for(int i = 0; i < simulation.getCoordinates().Length; i++)
            {
                drawBalls[i].x = simulation.getCoordinates()[i][0]; // Aktualizacja pozycji X
                drawBalls[i].y = simulation.getCoordinates()[i][1]; // Aktualizacja pozycji Y
                //Debug.WriteLine(drawBalls[i].x + " " + drawBalls[i].y);
            }
        }



        public override void StartSimulation()
        {
            simulation.startSimulation();
        }

        public override void StopSimulation()
        {
            simulation.stopSimulation();
        }

        /*        public override float[][] getCoordinates()
                {
                    return simulation.getCoordinates();
                }*/

        public override IDisposable Subscribe(IObserver<IBall> observer)
        {
            return eventObservable.Subscribe(x => observer.OnNext(x.EventArgs.Ball), ex => observer.OnError(ex), () => observer.OnCompleted());
        }


        /*        private void OnPropertyChanged(PropertyChangedEventArgs args)
       {
           PropertyChanged?.Invoke(this, args);
       }
       private void RelayBallUpdate(object source, PropertyChangedEventArgs args)
       {

           this.OnPropertyChanged(args);
       }*/
    }
}
