using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private float size { get; }
        private float density { get; }
        private float Xvelocity { get; }
        private float Yvelocity { get; }
        private float x {  get; }
        private float y { get; }

        public Ball(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.Xvelocity = randomVelocity();
            this.Xvelocity = randomVelocity();

        }

        private float randomVelocity() {
            Random rnd = new Random();
            return ((float)rnd.Next(0, 200)) / 10 - 10  ;
        }


        //Change position w Logic Layer


    }
}
