using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Board: DataAbstractAPI
    {
        public int sizeX { get; private set; }
        public int sizeY { get; private set; }

        public IBall[] balls;

        public override IBall[] getBalls()
        {
            return balls;
        }

        public void setBoardParameters(int x, int y, int ballsAmount)
        {
            sizeX = x;
            sizeY = y;
            createBalls(x, y, ballsAmount);
        }

        public void createBalls(int maxX, int maxY, int amount)
        {
            IBall[] balls = new IBall[amount];
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = new Ball(maxX, maxY);
            }
        }

        public Board(int maxX, int maxY, int amount) 
        {
            sizeX = maxX;
            sizeY = maxY;
            createBalls(maxX, maxY, amount);
        } //di workaround

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
        public override float[][] getCoordinates()
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
