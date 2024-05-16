using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IBall
    {
        Vector2 pos { get; }
        float r { get; }
        #nullable enable
        event EventHandler<ModelEventArgs>? ChangedPosition;
    }
    public class BallChangeEventArgs : EventArgs
    {
        public IBall Ball { get; internal set; }
    }
}
