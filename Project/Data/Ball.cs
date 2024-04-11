using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Ball : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private float size { get; }
        private float density { get; }
        private float Xvelocity;
        private float Yvelocity;
        public float x { get; set; }
        public float y { get; set; }

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
        public Ball()
        {
            this.x = randomPosition(300);
            this.y = randomPosition(100);
            this.Xvelocity = randomVelocity();
            this.Xvelocity = randomVelocity();

        }

        private float randomPosition(int maxPositon) {
            return ((float)rnd.Next(0, maxPositon) / 10);
        }

        private float randomVelocity() {

            return ((float)rnd.Next(0, 200)) / 10 - 10  ;
        }


        //Change position w Logic Layer


    }
}
