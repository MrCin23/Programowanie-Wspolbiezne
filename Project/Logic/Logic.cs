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

        static public Ball[] createBalls(int maxX, int maxY, int amount)
        {
            Ball[] balls = new Data.Ball[amount];
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = new Ball(maxX, maxY);
            }
            return balls;
        }

        static public void changeXdirection(Ball ball)
        {
            ball.setXVelocity(0-ball.getXVelocity());
        }

        static public void changeYdirection(Ball ball)
        {
            ball.setYVelocity(0-ball.getYVelocity());
        }

        static public void updatePosition(Ball ball)
        {
            //ball.x += ball.getXVelocity() * (1 / frequency);
            //ball.y += ball.getYVelocity() * (1 / frequency);
            ball.x += ball.getXVelocity() * 0.01f;
            ball.y += ball.getYVelocity() * 0.01f;
        }

        static public void updateBoard(Board board)
        {
            for (int i = 0; i < board.balls.Length; i++)
            {
                board.balls[i].x = board.balls[i].x + board.balls[i].getXVelocity() * 0.01f;
                board.balls[i].y = board.balls[i].y + board.balls[i].getYVelocity() * 0.01f;
            }
        }
    }
}
