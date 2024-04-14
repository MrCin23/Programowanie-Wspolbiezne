using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

        public static LogicAbstractAPI CreateLogicAPI(int x, int y, int amount)
        {
            return new Simulation(new Board(x, y, amount));
        }
        public abstract float[][] getCoordinates();
    }

    public class Logic
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

        static public void changeXdirection(DataAbstractAPI ball)
        {
            ball.setXVelocity(-ball.getXVelocity());
            //ball.RaisePropertyChanged(nameof(ball.getXVelocity));
            //additional update unnecessary
        }

        static public void changeYdirection(DataAbstractAPI ball)
        {
            ball.setYVelocity(-ball.getYVelocity());
            //ball.RaisePropertyChanged(nameof(ball.getYVelocity));
        }

        static public void updatePosition(DataAbstractAPI ball)
        {
            ball.x += ball.getXVelocity();
            ball.y += ball.getYVelocity();

            ball.RaisePropertyChanged(nameof(ball.x));
            ball.RaisePropertyChanged(nameof(ball.y));
        }

        static public void updateBoard(Board board)
        {
            for (int i = 0; i < board.balls.Length; i++)
            {
                updatePosition(board.balls[i]);
            }
        }
    }
}