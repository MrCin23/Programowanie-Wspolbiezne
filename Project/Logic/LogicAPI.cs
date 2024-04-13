using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public abstract Board getBoard();
        public abstract bool isRunning();
        public abstract void startSimulation();
        
        public abstract void stopSimulation();
        
        public static LogicAbstractAPI CreateLogicAPI(int x, int y, int amount)
        {
            return new Simulation(new Board(x, y, amount));
        }
        //public abstract ObservableCollection<DataAbstractAPI> observableDataProperty { get; set; }
        //public abstract Board boardProperty { get; set; }
        public abstract float[][] getCoordinates();
    }
}
