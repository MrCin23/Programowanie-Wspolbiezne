using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class Simulation
    {
        private float frequency = 100;
        private Board board;

        public void changePosition(Board board)
        {
            for (int i = 0; i < board.balls.Length; i++)
            {
                board.balls[i].x = board.balls[i].x + board.balls[i].getXVelocity() * (1 / frequency);
                board.balls[i].y = board.balls[i].y + board.balls[i].getYVelocity() * (1 / frequency);
            }
        }

        public Ball detectBandCollision(Board board)
        {
            return null;
        }
    }
}
