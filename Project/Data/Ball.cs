using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Ball : DataAbstractAPI, INotifyPropertyChanged
    {
        public override event PropertyChangedEventHandler PropertyChanged;

        private float size;
        private float density { get; }
        private float Xvelocity;
        private float Yvelocity;
        public float x { get; set; }
        public float y { get; set; }
        public static readonly float maxVelocity = 20.0f;

        public override float getSize() 
        { 
            return size; 
        }

        public override float getXVelocity()
        {
            return Xvelocity;
        }

        public override float getYVelocity()
        {
            return Yvelocity;
        }

        public override void setXVelocity(float xVelocity)
        {
            Xvelocity = xVelocity;
        }

        public override void setYVelocity(float yVelocity)
        {
            Yvelocity = yVelocity;
        }

        Random rnd = new Random();
        public Ball(int maxX, int maxY)
        {
            Console.WriteLine(this.size);
            this.size = 5.0f;
            this.x = randomPosition(maxX);
            this.y = randomPosition(maxY);
            this.Xvelocity = randomVelocity();
            this.Xvelocity = randomVelocity();
        }

        private float randomPosition(int maxPositon) {
            return (float)rnd.NextDouble() * (maxPositon - this.size) + this.size/2;
            //return (float)rnd.Next((int)(this.size * 5), (int)(maxPositon * 10 - this.size * 5)) / 10.0f;
        }

        private float randomVelocity() {
            return (float)(rnd.NextDouble() * (maxVelocity) - maxVelocity/2);
        }
    }
}
