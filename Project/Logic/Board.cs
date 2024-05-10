using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
/*
    internal class Board : IBoard
    {
        public int sizeX { get; private set; }
        public int sizeY { get; private set; }

        public IBall[] balls;

        public IBall[] getBalls()
        {
            return balls;
        }

        public void setBoardParameters(int x, int y, int ballsAmount)
        {
            sizeX = x;
            sizeY = y;
            balls = Logic.createBalls(x, y, ballsAmount);
        }

        public Board(){} //di workaround

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

        public void lookForCollisions()
        {
            while(true)
            {

            }
        }

        public static void Collision(IBall ball1, IBall ball2)
        {
            // Oblicz wektor normalny
            float dx = ball2.x - ball1.x;
            float dy = ball2.y - ball1.y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy); // odległość między kulami
            float n_x = dx / distance; // składowa x wektora normalnego
            float n_y = dy / distance; // składowa y wektora normalnego

            // Oblicz wektor styczny
            float t_x = -n_y; // składowa x wektora stycznego
            float t_y = n_x;  // składowa y wektora stycznego

            // Prędkości wzdłuż normalnej i stycznej
            float v1n = ball1.getXVelocity() * n_x + ball1.getYVelocity() * n_y;
            float v1t = ball1.getXVelocity() * t_x + ball1.getYVelocity() * t_y;

            float v2n = ball2.getXVelocity() * n_x + ball2.getYVelocity() * n_y;
            float v2t = ball2.getXVelocity() * t_x + ball2.getYVelocity() * t_y;

            // Nowe prędkości wzdłuż normalnej po zderzeniu
            float u1n = ((ball1.getMass() - ball2.getMass()) * v1n + 2 * ball2.getMass() * v2n) / (ball1.getMass() + ball2.getMass());
            float u2n = ((ball2.getMass() - ball1.getMass()) * v2n + 2 * ball1.getMass() * v1n) / (ball2.getMass() + ball1.getMass());

            // Nowe prędkości całkowite dla każdej kuli
            ball1.setXVelocity(u1n * n_x + v1t * t_x);
            ball1.setXVelocity(u1n * n_y + v1t * t_y);

            ball2.setXVelocity(u2n * n_x + v2t * t_x);
            ball2.setXVelocity(u2n * n_y + v2t * t_y);
        }
    }*/
}
