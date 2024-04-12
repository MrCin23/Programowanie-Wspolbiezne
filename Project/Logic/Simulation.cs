using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class Simulation : LogicAbstractAPI
    {

        private Board board;
        private bool running;
        private List<Thread> threads = new List<Thread>();

        public Simulation(Board board) 
        { 
            this.board = board;
            this.running = false;
        }

        public override Board getBoard()
        {
            return board;
        }

        public override bool isRunning() { return running; }

        public override void startSimulation()
        {
            if(!running)
            {
                this.running = true;
            }
            mainLoop();
        }

        public override void stopSimulation() 
        { 
            if(running)
            {
                this.running = false;
            }
        }

        private void mainLoop()
        {
            foreach(var ball in board.getBalls())
            {
                Thread thread = new Thread(() =>
                {
                    while (this.running)
                    {
                        this.board.checkBorderCollision();
                        Logic.updatePosition(ball);
                        Thread.Sleep(10);
                    }
                });
                thread.IsBackground = true;
                thread.Start();
                threads.Add(thread);
            }
        }

        public override float[][] getCoordinates()
        {
            return board.getCoordinates();
        }

    }
}
