using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IBoard
    {
        IBall[] getBalls();
        void checkBorderCollision();
        float[][] getCoordinates();
    }

    internal class Board : DataAbstractAPI
    {
        public override int sizeX { get; set; }
        public override int sizeY { get; set; }

        public IBall[] balls;

        public override IBall[] getBalls()
        {
            return balls;
        }

        public override void setBalls(IBall[] balls)
        {
            this.balls = balls;
        }

        public override void setBoardParameters(int x, int y, int ballsAmount)
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
            this.balls = balls;
        }


        public Board() //int maxX, int maxY, int amount
        {
/*            sizeX = maxX;
            sizeY = maxY;
            createBalls(maxX, maxY, amount);*/
        } //di workaround

        public override void updatePosition(IBall ball) 
        {
            foreach (IBall b in balls)
            {
                if (b == ball)
                {
                    ball.updatePosition();
                    ball.RaisePropertyChanged(nameof(ball.x));
                    ball.RaisePropertyChanged(nameof(ball.y));
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
}
