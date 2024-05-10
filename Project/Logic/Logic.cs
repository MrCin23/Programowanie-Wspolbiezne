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
        internal abstract DataAbstractAPI getBoard();
        public abstract bool isRunning();
        public abstract void startSimulation();

        public abstract void stopSimulation();

        public abstract IBall[] getBalls();

/*        internal static DataAbstractAPI CreateBoard(int x, int y, int ballsAmount)
        {
            return new DataAbstractAPI(x, y, ballsAmount);
        }*/
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
        static public IBall[] createBalls(int maxX, int maxY, int amount)
        {
            DataAbstractAPI[] balls = new DataAbstractAPI[amount];
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = DataAbstractAPI.CreateDataAPI(); //maxX, maxY, amount
            }
            return (IBall[])balls;
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


/*        static internal void updateBoard(IBoard board)
        {
            for (int i = 0; i < board.getBalls().Length; i++)
            {
                updatePosition(board.getBalls()[i]);
            }
        }*/
    }
}