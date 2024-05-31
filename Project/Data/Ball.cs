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
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Data
{
    public interface IBall
    {
        Vector2 Pos { get; }
        Vector2 vel { get; set; }
        float getSize();
        float getMass();
        void destroy();
        #nullable enable
        event EventHandler<DataEventArgs>? ChangedPosition;
    }

    internal class Ball : IXmlSerializable, IBall
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
        Stopwatch stopwatch;
        private DataLogger logger;


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
        public Ball(int maxX, int maxY, DataLogger logger)
        {
            this.logger = logger;
            this.size = 10.0f;
            this.density = 10;
            this.pos = new Vector2(randomPosition(maxX), randomPosition(maxY));
            this.vel = new Vector2(randomVelocity(), randomVelocity());
            this.running = true;
            stopwatch = new Stopwatch();
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
                        logger.addToQueue(this);
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

        public XmlSchema GetSchema() => null;

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

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement();

            float posX = float.Parse(reader.GetAttribute("PosX"));
            float posY = float.Parse(reader.GetAttribute("PosY"));

            float velX = float.Parse(reader.GetAttribute("VelX"));
            float velY = float.Parse(reader.GetAttribute("VelY"));
            
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Ball");

            writer.WriteAttributeString("PosX", pos.X.ToString());
            writer.WriteAttributeString("PosY", pos.Y.ToString());
            writer.WriteAttributeString("VelX", vel.X.ToString());
            writer.WriteAttributeString("VelY", vel.Y.ToString());

            writer.WriteEndElement();
        }
    }
}
