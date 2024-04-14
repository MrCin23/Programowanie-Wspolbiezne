using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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
    }


    internal class Model : ModelAbstractAPI
    {
        private IObservable<EventPattern<BallChangeEventArgs>> eventObservable = null;
        public LogicAbstractAPI simulation { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        //public IObservable<LogicAbstractAPI> observableLogicAPI;
        //public ObservableCollection<BallToDraw> drawBalls;


        public Model(LogicAbstractAPI api, int amount)
        {
            eventObservable = Observable.FromEventPattern<BallChangeEventArgs>(this, "BallChanged");
            simulation = api;
            for (int i = 0; i < amount; i++)
            {
                DrawBalls ball = new DrawBalls(api.getCoordinates()[i][0], api.getCoordinates()[i][1]);
            }
            /*db = new DrawBalls(simulation);
            drawBalls = db.ballsToDraw;
            //Debug.WriteLine();
            foreach (var ball in drawBalls)
            {
                Debug.WriteLine(ball.X + " " + ball.Y);
            }*/
        }

/*        public Model()
        {
            eventObservable = Observable.FromEventPattern<BallChangeEventArgs>(this, "Ball Changed");
        }*/

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
