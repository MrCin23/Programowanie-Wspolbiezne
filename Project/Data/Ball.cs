using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public interface IBall
    {
        Vector2 pos { get; }
        Vector2 vel { get; }
        float getSize();
        float getXVelocity();
        float getYVelocity();
        float getMass();
        void RaisePropertyChanged(Vector2 pos);
        event PropertyChangedEventHandler PropertyChanged;
        void updatePosition();
    }

    internal class Ball : INotifyPropertyChanged, IBall
    {
        public event EventHandler<DataEventArgs>? ChangedPosition;

        private float size { get; set; }
        private float density { get; set; }
        public Vector2 pos { get; private set; }
        public Vector2 vel { get; private set; }
        public static readonly float maxVelocity = 2.0f;

        public float getSize() 
        { 
            return size; 
        }

        Random rnd = new Random();
        public Ball(int maxX, int maxY)
        {
            this.size = 10.0f;
            this.density = 10;
            this.pos = new Vector2(randomPosition(maxX), randomPosition(maxY));
            this.vel = new Vector2(randomVelocity(), randomVelocity());
        }

        private float randomPosition(int maxPositon) {
            return (float)rnd.NextDouble() * (maxPositon - this.size);
        }

        private float randomVelocity() {
            return (float)(rnd.NextDouble() * (maxVelocity) - maxVelocity/2);
        }
        public void RaisePropertyChanged(Vector2 pos)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pos));
        }

        public float getMass()
        {
            return (float)(4 / 3 * Math.PI * Math.Pow(size/2, 3)) * density; 
        }

        public void updatePosition()
        {
            Thread thread = new Thread(() =>
            {
                while (this.running)
                {
                    checkBorderCollisionForBall(this);
                    //this.PropertyChanged += RelayBallUpdate; do przerobki
                    //Thread.Sleep(10); do przerobki
                }
            });
            thread.IsBackground = true;
            thread.Start();
            x += getXVelocity();
            y += getYVelocity();

            RaisePropertyChanged(x,y);
            RaisePropertyChanged(nameof(y));
        }
    }
}
