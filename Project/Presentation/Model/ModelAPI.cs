using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
    }


    internal class Model: ModelAbstractAPI
    {
        public LogicAbstractAPI simulation;
        public IObservable<LogicAbstractAPI> observableLogicAPI;

        public Model(LogicAbstractAPI api) {
            simulation = api;
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
