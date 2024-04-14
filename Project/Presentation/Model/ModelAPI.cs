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
    public abstract class ModelAbstractAPI : IObservable<IBall>
    {
        public static ModelAbstractAPI CreateModelAPI(int x, int y, int amount)
        {
            LogicAbstractAPI LAAPI = LogicAbstractAPI.CreateLogicAPI(700, 300, amount);
            Model model = new Model(LAAPI, amount);
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
        DrawBalls[] drawBalls;

        public override DrawBalls[] getballs()
        {
            return drawBalls;
        }

        public Model(LogicAbstractAPI api, int amount)
        {
            eventObservable = Observable.FromEventPattern<BallChangeEventArgs>(this, "BallChanged");
            simulation = api;
            drawBalls = new DrawBalls[amount];
            for (int i = 0; i < amount; i++)
            {
                DrawBalls ball = new DrawBalls(api.getCoordinates()[i][0], api.getCoordinates()[i][1]);
                drawBalls[i] = ball;
                api.PropertyChanged += OnBallChanged; //send update to upper level
            }
        }
        private void OnBallChanged(object sender, PropertyChangedEventArgs args)
        {
            //reaction to update from layers below
            if (drawBalls[0].x != simulation.getCoordinates()[0][0] && drawBalls[0].y != simulation.getCoordinates()[0][1])
            {
                UpdatePosition();
            }
        }
        private void UpdatePosition()
        {
            for(int i = 0; i < simulation.getCoordinates().Length; i++)
            {
                drawBalls[i].x = simulation.getCoordinates()[i][0];
                drawBalls[i].y = simulation.getCoordinates()[i][1];
            }
        }

        public override void StartSimulation()
        {
            //button
            simulation.startSimulation();
        }

        public override void StopSimulation()
        {
            //button
            simulation.stopSimulation();
        }
        
        //source: https://github.com/mpostol/TP
        //presentation model sublayers are heavily inspired by that source, but implemented to fit our code
        public override IDisposable Subscribe(IObserver<IBall> observer)
        {
            return eventObservable.Subscribe(x => observer.OnNext(x.EventArgs.Ball), ex => observer.OnError(ex), () => observer.OnCompleted());
        }
    }
}
