using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    internal class Simulation : LogicAbstractAPI
    {
        
        private Board board;
        private bool running;

        public Simulation(Board board) 
        { 
            this.board = board;
            this.running = false;
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
            while(this.running)
            {
                this.board.checkBorderCollision();
                Logic.updateBoard(this.board);
                Thread.Sleep(10);
            }
        }

        public override float[][] getCoordinates()
        {
            return board.getCoordinates();
        }

    }
}
