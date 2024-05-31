using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class BallAPI : IBall
    {
        public abstract Vector2 Pos { get; }
        public abstract Vector2 vel { get; set; }

        public abstract event EventHandler<DataEventArgs> ChangedPosition;

/*        public static BallAPI CreateBall() {
            return new Ball();
        }*/

        public abstract void destroy();
        public abstract float getMass();
        public abstract float getSize();
    }
}
