using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAbstractAPI
    {
        public abstract int sizeX { get; set; }
        public abstract int sizeY { get; set; }
        public static DataAbstractAPI CreateDataAPI()
        {
            return new Board();
        }
        public abstract IBall[] getBalls();
        public abstract void setBalls(IBall[] balls);
        public abstract Vector2[] getCoordinates();
        public abstract void setBoardParameters(int x, int y, int ballsAmount);
        public abstract void clear();
        public abstract void stopLogger();
    }
}
