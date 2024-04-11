using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    internal class Simulation
    {
        
        private Board board;
        private bool running;

        public Simulation(Board board) 
        { 
            this.board = board;
            this.running = false;
        }

        public bool isRunning() { return running; }

        public void startSimulation()
        {
            if(!running)
            {
                this.running = true;
            }
            mainLoop();
        }

        public void stopSimulation() 
        { 
            if(running)
            {
                this.running = false;
            }
            mainLoop();
        }


        public void mainLoop()
        {
            while(this.running)
            {
                this.board.checkBorderCollision();
                Logic.updateBoard(this.board);
                Thread.Sleep(10);
            }
        }

    }
}
