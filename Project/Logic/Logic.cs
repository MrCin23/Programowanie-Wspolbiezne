using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class Logic
    {

        public Logic() { }

        Ball[] createBalls(int amount)
        {
            Ball[] balls = new Data.Ball[amount];
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = new Ball();
            }
            return balls;
        }

    }
}
