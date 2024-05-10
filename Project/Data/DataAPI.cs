﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{

    //Chyba szykuje się duży refaktoring pt. Zmiany DataAPI z Ball na Board

    public abstract class DataAbstractAPI
    {
        public static DataAbstractAPI CreateDataAPI(int maxX, int maxY, int amount)
        {
            return new Board(maxX, maxY, amount);
        }
        public abstract IBall[] getBalls();
        public abstract float[][] getCoordinates();
    }

    /*public abstract class DataAbstractAPI : IBall
    {
        public abstract float x { get; set; }
        public abstract float y { get; set; }
        public abstract event PropertyChangedEventHandler PropertyChanged;
        public abstract float getSize();
        public abstract float getXVelocity();
        public abstract float getYVelocity();
        public abstract void setXVelocity(float xVelocity);
        public abstract void setYVelocity(float yVelocity);
        public static DataAbstractAPI CreateDataAPI(int maxX, int maxY)
        {
            return new Ball(maxX, maxY);
        }
        public abstract void RaisePropertyChanged([CallerMemberName] string propertyName = "");
        public abstract float getMass();
    }*/
}
