using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

[assembly: InternalsVisibleTo("Program.XmlSerializers")]
namespace Data
{
    internal class DataLogger
    {
        private ConcurrentQueue<IBall> ballsQueue;
        private string filename;
        private CancellationTokenSource StateChange = new CancellationTokenSource();
        private bool isRunning;

        public DataLogger(string filename = "Logger.xml")
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
            this.filename = Path.Combine(path, filename);
            File.Create(this.filename).Close();
            ballsQueue = new ConcurrentQueue<IBall>();
            this.isRunning = true;
            Task.Run(writeDataToLogger);
        }

        public void stopRunning()
        {
            isRunning = false;
        }

        public void addToQueue(IBall ball)
        {
            ballsQueue.Enqueue(ball);
            StateChange.Cancel();
        }

        public async void writeDataToLogger()
        {
            bool isRootWritten = false;
            XmlWriter writer = null;
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filename, true))
                {
                    writer = XmlWriter.Create(streamWriter, new XmlWriterSettings() { Indent = true, OmitXmlDeclaration = true });
                    writer.WriteStartElement("Balls");
                    writer.Flush();
                    isRootWritten = false;

                    while (this.isRunning)
                    {
                        if (!ballsQueue.IsEmpty)
                        {
                            while (ballsQueue.TryDequeue(out IBall ball))
                            {
                                DataContractSerializer xmlSer = new DataContractSerializer(typeof(Ball));
                                xmlSer.WriteObject(writer, ball);
                                writer.Flush();
                                await Task.Delay(Timeout.Infinite, StateChange.Token).ContinueWith(_ => { });
                            }
                        }
                    }
                    isRootWritten = true;
                    writer.WriteEndElement();
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log: {ex.Message}");
            }
            finally
            {
                if (!isRootWritten && writer != null)
                {
                    writer.WriteEndElement();
                    writer.Flush();
                    writer.Close();
                }
            }
        }
    }
}
