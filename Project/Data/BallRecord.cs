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
        public float posX { get; private set; }
        [DataMember]
        public float posY { get; private set; }
        [DataMember]
        public float velX { get; private set; }
        [DataMember]
        public float velY { get; private set; }
        [DataMember]
        public int id { get; private set; }
        [DataMember]
        public DateTime time { get; private set; }
        internal BallRecord(float posX, float posY, float velX, float velY, int id, DateTime time)
        {
            this.posX = posX;
            this.posY = posY;
            this.velX = velX;
            this.velY = velY;
            this.id = id;
            this.time = time;
        }
    }
}
