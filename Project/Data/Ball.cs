using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

[assembly: InternalsVisibleTo("Program.XmlSerializers")]
namespace Data
{
    public interface IBall
    {
        Vector2 Pos { get; }
        Vector2 vel { get; set; }
        int ID { get; }
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
        public static readonly float maxVelocity = 0.2f;
        private bool running;
        private Thread thread;
        Stopwatch stopwatch;
        private DataLogger logger;
        private readonly Object lockObject = new Object();
        public int ID { get; }


        public Vector2 Pos
        {
            get => pos;
        }
        public Ball(){}

        public float getSize() 
        { 
            return size; 
        }

        Random rnd = new Random();
        public Ball(int ID, int maxX, int maxY)
        {
            this.ID = ID;
            this.size = 10.0f;
            this.density = 10;
            this.pos = new Vector2(randomPosition(maxX), randomPosition(maxY));
            this.vel = new Vector2(randomVelocity(), randomVelocity());
            this.running = true;
            stopwatch = new Stopwatch();
            this.logger = DataLogger.GetInstance();
            this.thread = new Thread(() =>
            {
                try
                {
                    while (this.running)
                    {
                        float time = stopwatch.ElapsedMilliseconds;
                        stopwatch.Restart();
                        stopwatch.Start();
                        move(time);
                        lock (lockObject)
                        {
                            logger.addToQueue(this);
                        }
                        int timeToSleep = (int)Math.Ceiling(Math.Sqrt(Math.Pow(this.vel.X * 100, 2) + Math.Pow(this.vel.Y * 100, 2)));
                        if (timeToSleep < 10)
                        {
                            timeToSleep = 10;
                        }
                        Thread.Sleep(timeToSleep);
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

        private void move(float time)
        {
            this.pos += time * vel;
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
                    move(10.0f);
                }
            }
        }

    }
}
