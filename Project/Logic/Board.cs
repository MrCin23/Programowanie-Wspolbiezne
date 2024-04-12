using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Board
    {
        public int sizeX {  get; }
        public int sizeY { get; }
        internal Ball[] balls;

        public Ball[] getBalls()
        {
            return balls;
        }

        public Board(int sizeX, int sizeY, int amount)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.balls = Logic.createBalls(sizeX, sizeY, amount);
        }

        public void checkBorderCollision() 
        {
            foreach (Ball ball in balls)
            {
                if (ball.x + ball.getSize() >= this.sizeX || ball.x + ball.getXVelocity() + ball.getSize() >= this.sizeX)
                {
                    Logic.changeXdirection(ball);
                    Logic.updatePosition(ball);
                }
                if (ball.y + ball.getSize() >= this.sizeY || ball.y + ball.getYVelocity() + ball.getSize() >= this.sizeY)
                {
                    Logic.changeYdirection(ball);
                    Logic.updatePosition(ball);
                }
            }
        }

        internal float[][] getCoordinates()
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
