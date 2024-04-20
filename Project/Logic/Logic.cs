using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("LogicTests")]
namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public abstract event PropertyChangedEventHandler PropertyChanged;
        internal abstract Board getBoard();
        public abstract bool isRunning();
        public abstract void startSimulation();

        public abstract void stopSimulation();

        public abstract IBall[] getBalls();

        internal static Board CreateBoard()
        {
            return new Board();
        }
        public abstract void getBoardParameters(int x, int y, int ballsAmount);

        public static LogicAbstractAPI CreateLogicAPI()
        {
            return new Simulation();
        }
        public abstract float[][] getCoordinates();
        public abstract void setBalls(IBall[] balls);
    }

    internal class Logic
    {
        static public DataAbstractAPI[] createBalls(int maxX, int maxY, int amount)
        {
            DataAbstractAPI[] balls = new DataAbstractAPI[amount];
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = DataAbstractAPI.CreateDataAPI(maxX, maxY);
            }
            return balls;
        }

        static public void changeXdirection(IBall ball)
        {
            ball.setXVelocity(-ball.getXVelocity());
            //ball.RaisePropertyChanged(nameof(ball.getXVelocity));
            //additional update unnecessary
        }

        static public void changeYdirection(IBall ball)
        {
            ball.setYVelocity(-ball.getYVelocity());
            //ball.RaisePropertyChanged(nameof(ball.getYVelocity));
        }

        static public void updatePosition(IBall ball)
        {
            ball.x += ball.getXVelocity();
            ball.y += ball.getYVelocity();

            ball.RaisePropertyChanged(nameof(ball.x));
            ball.RaisePropertyChanged(nameof(ball.y));
        }

        static internal void updateBoard(Board board)
        {
            for (int i = 0; i < board.balls.Length; i++)
            {
                updatePosition(board.balls[i]);
            }
        }
    }
}