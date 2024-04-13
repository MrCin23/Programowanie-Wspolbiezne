using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Model
{
    public abstract class ModelAbstractAPI 
    {
        public static ModelAbstractAPI CreateModelAPI(int x, int y, int amount)
        {
            //return null;
            return new Model(LogicAbstractAPI.CreateLogicAPI(x, y, amount));
        }
        public abstract void startSimulation();
        public abstract void stopSimulation();
        public abstract float[][] getCoordinates();

        public abstract Object[] getData();
    }


    internal class Model: ModelAbstractAPI
    {
        public LogicAbstractAPI simulation { get; set; }
        public Object[] data { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        //public IObservable<LogicAbstractAPI> observableLogicAPI;
        public ObservableCollection<BallToDraw> drawBalls;
        DrawBalls db;

        public override Object[] getData()
        {
            return data;
        }

        public Model(LogicAbstractAPI api) {
            simulation = api;
            db = new DrawBalls(simulation);
            data = simulation.getBoard().getBalls();
            drawBalls = db.ballsToDraw;
            //Debug.WriteLine();
            foreach (var ball in drawBalls)
            {
                Debug.WriteLine(ball.X + " " + ball.Y);
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
