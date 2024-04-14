using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public override float x { get; set; }
        public override float y { get; set; }
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
            this.size = 10.0f;
            this.x = randomPosition(maxX);
            this.y = randomPosition(maxY);
            this.Xvelocity = randomVelocity();
            this.Yvelocity = randomVelocity();
            //Debug.WriteLine(x + " " + y + " " + Xvelocity + " " + Yvelocity);
        }

        private float randomPosition(int maxPositon) {
            return (float)rnd.NextDouble() * (maxPositon - this.size) + this.size/2;
            //return (float)rnd.Next((int)(this.size * 5), (int)(maxPositon * 10 - this.size * 5)) / 10.0f;
        }

        private float randomVelocity() {
            return (float)(rnd.NextDouble() * (maxVelocity) - maxVelocity/2);
        }
        public override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
