using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class ModelAbstractAPI 
    {
        public abstract ObservableCollection<BallToDraw> drawBalls { get; }
        public static ModelAbstractAPI CreateModelAPI(int x, int y, int amount)
        {
            //return null;
            return new Model(LogicAbstractAPI.CreateLogicAPI(x, y, amount));
        }
        public abstract void startSimulation();
        public abstract void stopSimulation();
        public abstract float[][] getCoordinates();
    }


    internal class Model: ModelAbstractAPI
    {
        public LogicAbstractAPI simulation;
        public IObservable<LogicAbstractAPI> observableLogicAPI;
        public override ObservableCollection<BallToDraw> drawBalls { get; }
        DrawBalls db;

        public Model(LogicAbstractAPI api) {
            simulation = api;
            db = new DrawBalls(simulation);
            drawBalls = db.ballsToDraw;
            foreach (var ball in drawBalls)
            {
                Debug.WriteLine(ball.x + " " + ball.y);

            }
        }

        public override void startSimulation()
        {
            simulation.startSimulation();
        }

        public override void stopSimulation()
        {
            simulation.stopSimulation();
        }

        public override float[][] getCoordinates()
        {
            return simulation.getCoordinates();
        }

    }
}
