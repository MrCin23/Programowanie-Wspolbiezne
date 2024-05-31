using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data
{
    internal class DataLogger
    {
        private ConcurrentQueue<IBall> ballsQueue;
        private string filename;
        private CancellationTokenSource StateChange = new CancellationTokenSource();
        bool isRunning;

        public DataLogger()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
            filename = Path.Combine(path, "Logger.xml");
            //File.Create(filename).Close();
            ballsQueue = new ConcurrentQueue<IBall>();
            this.isRunning = true;
            Task.Run(writeDataToLogger);
        }

        public void addToQueue(IBall ball)
        {
            ballsQueue.Enqueue(ball);
            StateChange.Cancel();
        }

        public async void writeDataToLogger()
        {
            while (this.isRunning)
            {
                if (!ballsQueue.IsEmpty)
                {
                    while (ballsQueue.TryDequeue(out IBall ball))
                    {
                        using (var writer = new StreamWriter(filename, true))
                        {
                            if (ball is Ball internalBall)
                            {
                                XmlSerializer xmlSer = new XmlSerializer(typeof(Ball));
                                xmlSer.Serialize(writer, internalBall);
                            }
                        }
                        await Task.Delay(Timeout.Infinite, StateChange.Token).ContinueWith(_ => { });
                    }
                }
            }
        }

    }
}
