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
    internal class DrawBalls : IBall, INotifyPropertyChanged
    {
        private Vector2 _pos;
        #nullable enable
        public event EventHandler<ModelEventArgs>? ChangedPosition;
        private LogicAbstractAPI api;

        public event PropertyChangedEventHandler? PropertyChanged;

        public double x
        {
            get { return _pos.X; } 
            set { 
                _pos.X = (float)value;
                //ModelEventArgs args = new ModelEventArgs(pos);
                //OnPropertyChanged(args);
                RaisePropertyChanged();
            }
        }
        public double y
        {
            get { return _pos.Y; }
            set
            {
                _pos.Y = (float)value;
                //ModelEventArgs args = new ModelEventArgs(pos);
                //OnPropertyChanged(args);
                RaisePropertyChanged();
            }
        }
        public Vector2 pos
        {
            get { return _pos; }
            internal set
            {
                if(_pos != value)
                {
                    _pos = value;
/*                   ModelEventArgs args = new ModelEventArgs(pos);
                    OnPropertyChanged(args);*/
                    RaisePropertyChanged();
                    //this is an update that directly affects ObservableCollection,
                    //which further fires an update to view, which then displays balls' positions
                }
            }
        }
        public LogicAbstractAPI Api { get; set; }

        public float r { get; internal set; }

        public DrawBalls(Vector2 pos)
        {
            this.pos = pos;
        }

        private void OnPropertyChanged(ModelEventArgs args)
        {
            //Debug.WriteLine(x + " " + y);
            ChangedPosition?.Invoke(this, args);
        }
        public void UpdateCircle(Object s, ModelEventArgs e)
        {
            IBall ball = (IBall)s;
            x = (int)ball.pos.X;
            y = (int)ball.pos.Y;
            RaisePropertyChanged();
        }

        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Debug.WriteLine(x + " " + y);
        }
    }
}
