using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Logic
    {
        static public readonly int frequency = 100;

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
            ball.RaisePropertyChanged(nameof(ball.getYVelocity));
        }

        static public void changeYdirection(DataAbstractAPI ball)
        {
            ball.setYVelocity(-ball.getYVelocity());
            ball.RaisePropertyChanged(nameof(ball.getYVelocity));
        }

        static public void updatePosition(DataAbstractAPI ball)
        {
            ball.x += ball.getXVelocity() * (1.0f / frequency);
            ball.y += ball.getYVelocity() * (1.0f / frequency);
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
