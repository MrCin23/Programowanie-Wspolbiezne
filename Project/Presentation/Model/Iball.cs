using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IBall : INotifyPropertyChanged
    {
        float x { get; }
        float y { get; }
        float r { get; }
    }
    public class BallChangeEventArgs : EventArgs
    {
        public IBall Ball { get; internal set; }
    }
}
