﻿using Data;
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
        public event EventHandler BallsChanged;
        public abstract Board getBoard();
        public abstract bool isRunning();
        public abstract void startSimulation();
        
        public abstract void stopSimulation();
        
        public static LogicAbstractAPI CreateLogicAPI(int x, int y, int amount)
        {
            return new Simulation(new Board(x, y, amount));
        }
        public abstract float[][] getCoordinates();
    }
}
