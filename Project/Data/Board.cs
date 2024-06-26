﻿using System.Numerics;

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

        public override void clear()
        {
            foreach (IBall ball in balls) 
            {
                ball.destroy();
            }
            balls = [];
        }

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
                balls[i] = new Ball(i, maxX, maxY);
            }
            this.balls = balls;
        }

        public override void stopLogger()
        {
            DataLogger.GetInstance().stopRunning();
        }

        public Board()
        {
        }

        public override Vector2[] getCoordinates()
        {
            Vector2[] coordinates = new Vector2[balls.Length];
            for (int i = 0; i < balls.Length; i++)
            {
                Vector2 pos = balls[i].Pos;
                coordinates[i] = pos;
            }
            return coordinates;
        }
    }
}
