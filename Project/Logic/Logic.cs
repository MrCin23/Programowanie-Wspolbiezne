using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("LogicTests")]
namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        #nullable enable
        public abstract event EventHandler<LogicEventArgs>? ChangedPosition;
        internal abstract DataAbstractAPI getBoard();
        public abstract void startSimulation();

        public abstract void stopSimulation();

        public abstract IBall[] getBalls();

        public abstract void getBoardParameters(int x, int y, int ballsAmount);

        public static LogicAbstractAPI CreateLogicAPI()
        {
            return new Simulation();
        }
        public abstract Vector2[] getCoordinates();
        public abstract void setBalls(IBall[] balls);
    }

    internal class Logic
    {
        static public void changeXdirection(IBall ball)
        {
            Vector2 vel = new Vector2(-ball.vel.X, ball.vel.Y);
            ball.vel = vel;
            //additional update unnecessary
        }

        static public void changeYdirection(IBall ball)
        {
            Vector2 vel = new Vector2(ball.vel.X, -ball.vel.Y);
            ball.vel = vel;
        }
    }
}