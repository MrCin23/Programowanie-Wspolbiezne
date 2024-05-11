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
    public interface IBall
    {
        float x { get; set; }
        float y { get; set; }
        float getSize();
        float getXVelocity();
        float getYVelocity();
        void setXVelocity(float xVelocity);
        void setYVelocity(float yVelocity);
        float getMass();
        void RaisePropertyChanged([CallerMemberName] string propertyName = null);
        event PropertyChangedEventHandler PropertyChanged;
        void updatePosition();
    }

    internal class Ball : INotifyPropertyChanged, IBall
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private float size { get; set; }
        private float density { get; set; }
        private float Xvelocity;
        private float Yvelocity;
        public float x { get; set; }
        public float y { get; set; }
        public static readonly float maxVelocity = 2.0f;

        public float getSize() 
        { 
            return size; 
        }

        public float getXVelocity()
        {
            return Xvelocity;
        }

        public float getYVelocity()
        {
            return Yvelocity;
        }

        public void setXVelocity(float xVelocity)
        {
            Xvelocity = xVelocity;
        }

        public void setYVelocity(float yVelocity)
        {
            Yvelocity = yVelocity;
        }

        Random rnd = new Random();
        public Ball(int maxX, int maxY)
        {
            this.size = 10.0f;
            this.density = 10;
            this.x = randomPosition(maxX);
            this.y = randomPosition(maxY);
            this.Xvelocity = randomVelocity();
            this.Yvelocity = randomVelocity();
        }

        private float randomPosition(int maxPositon) {
            return (float)rnd.NextDouble() * (maxPositon - this.size);
        }

        private float randomVelocity() {
            return (float)(rnd.NextDouble() * (maxVelocity) - maxVelocity/2);
        }
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public float getMass()
        {
            return (float)(4 / 3 * Math.PI * Math.Pow(size/2, 3)) * density; 
        }

        public void updatePosition()
        {
            lock (this)
            {
                x += getXVelocity();
                y += getYVelocity();

                RaisePropertyChanged(nameof(x));
                RaisePropertyChanged(nameof(y));
            }
        }
    }
}
