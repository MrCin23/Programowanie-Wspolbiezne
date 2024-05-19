using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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
        Vector2 vel { get; set; }
        float getSize();
        float getMass();
        void destroy();
        #nullable enable
        event EventHandler<DataEventArgs>? ChangedPosition;
    }

    internal class Ball : IBall
    {
        #nullable enable
        public event EventHandler<DataEventArgs>? ChangedPosition;

        private float size { get; set; }
        private float density { get; set; }
        public Vector2 pos { get; private set; }
        private Vector2 _vel { get; set; }
        public static readonly float maxVelocity = 2.0f;
        private bool running;
        private Thread thread;

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
            this.running = true;
            this.thread = new Thread(() =>
            {
                try
                {
                    while (this.running)
                    {
                        move();
                        Thread.Sleep(10);
                    }
                }
                catch (ThreadInterruptedException)
                {
                    Debug.WriteLine("Thread killed");
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        private float randomPosition(int maxPositon) {
            return (float)rnd.NextDouble() * (maxPositon - 2*this.size);
        }

        private float randomVelocity() {
            return (float)(rnd.NextDouble() * (maxVelocity) - maxVelocity/2);
        }

        public float getMass()
        {
            return (float)(4 / 3 * Math.PI * Math.Pow(size/2, 3)) * density; 
        }

        private void move()
        {
            this.pos += vel;
            DataEventArgs args = new DataEventArgs(pos);
            ChangedPosition?.Invoke(this, args);
        }

        public void destroy()
        {
            this.running = false;
            this.thread.Interrupt();
        }

        public Vector2 vel
        {
            get { return _vel; }
            set
            {
                if (_vel != value)
                {
                    _vel = value;
                    move();
                }
            }
        }
    }
}
