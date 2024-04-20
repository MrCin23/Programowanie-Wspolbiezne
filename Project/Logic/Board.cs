using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IBoard
    {
        DataAbstractAPI[] getBalls();
        void checkBorderCollision();
        float[][] getCoordinates();
    }

    internal class Board : IBoard
    {
        public int sizeX { get; private set; }
        public int sizeY { get; private set; }

        public DataAbstractAPI[] balls;

        public DataAbstractAPI[] getBalls()
        {
            return balls;
        }

        public void setBoardParameters(int x, int y, int ballsAmount)
        {
            sizeX = x;
            sizeY = y;
            balls = Logic.createBalls(x, y, ballsAmount);
        }

        public Board()
        {
        }

        public void checkBorderCollision() 
        {
            foreach (var ball in balls)
            {
                if (ball.x + ball.getSize() >= this.sizeX || ball.x + ball.getXVelocity() + ball.getSize() >= this.sizeX || 
                    ball.x <= 0 || ball.x + ball.getXVelocity() <= 0)
                {
                    Logic.changeXdirection(ball);
                    Logic.updatePosition(ball);
                }
                if (ball.y + ball.getSize() >= this.sizeY || ball.y + ball.getYVelocity() + ball.getSize() >= this.sizeY || 
                    ball.y <= 0 || ball.y + ball.getYVelocity() <= 0)
                {
                    Logic.changeYdirection(ball);
                    Logic.updatePosition(ball);
                }
            }
        }
        public float[][] getCoordinates()
        {
            float[][] coordinates = new float[balls.Length][];
            for (int i = 0; i < balls.Length; i++)
            {
                float[] a = new float[2];
                a[0] = balls[i].x;
                a[1] = balls[i].y;
                coordinates[i] = a;
            }
            return coordinates;
        }
    }
}
