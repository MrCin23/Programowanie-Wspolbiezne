using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public abstract event PropertyChangedEventHandler PropertyChanged;
        public abstract Board getBoard();
        public abstract bool isRunning();
        public abstract void startSimulation();
        
        public abstract void stopSimulation();
        
        public static LogicAbstractAPI CreateLogicAPI(int x, int y, DataAbstractAPI[] balls)
        {
            return new Simulation(new Board(x, y, balls));
        }
        public abstract float[][] getCoordinates();
    }
}
