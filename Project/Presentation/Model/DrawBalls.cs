using Data;
using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    internal class DrawBalls : IBall
    {
        #nullable enable
        public event EventHandler<ModelEventArgs>? ChangedPosition;
        private LogicAbstractAPI api;
        public Vector2 pos 
        { 
            get { return pos; }
            internal set
            {
                if(pos != value)
                {
                    pos = value;
                    ModelEventArgs args = new ModelEventArgs(pos);
                    OnPropertyChanged(args);
                    //this is an update that directly affects ObservableCollection,
                    //which further fires an update to view, which then displays balls' positions
                }
            }
        }
        public LogicAbstractAPI Api { get; set; }

        public float r { get; internal set; }

        public DrawBalls(float xpos, float ypos)
        {
            Vector2 pos = new Vector2(xpos, ypos);
            this.pos = pos;
        }

        private void OnPropertyChanged(ModelEventArgs args)
        {
            ChangedPosition?.Invoke(this, args);
        }
    }
}
