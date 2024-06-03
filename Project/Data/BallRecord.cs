using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [DataContract]
    internal record struct BallRecord
    {
        [DataMember]
        public float posX { get; }
        [DataMember]
        public float posY { get; }
        [DataMember]
        public float velX { get; }
        [DataMember]
        public float velY { get; }
        [DataMember]
        public int id { get; }
        [DataMember]
        public string time { get; }
        internal BallRecord(float posX, float posY, float velX, float velY, int id, DateTime time)
        {
            this.posX = posX;
            this.posY = posY;
            this.velX = velX;
            this.velY = velY;
            this.id = id;
            this.time = time.ToString();
        }
    }
}
