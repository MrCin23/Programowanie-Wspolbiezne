using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /*public class BallToDraw
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private float x;
        private float y;

        public float X
        {
            get => x;
            set
            {
                x = value;
                PropertyChanged += RelayBallUpdate;
            }
        }

        public float Y
        {
            get => y;
            set
            {
                y = value;
                PropertyChanged += RelayBallUpdate;
            }
        }
        private void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        public BallToDraw(float x, float y) {
            this.x = x; 
            this.y = y;
        }

        private void RelayBallUpdate(object source, PropertyChangedEventArgs args)
        {
            this.OnPropertyChanged(args);
        }
    }*/

    public class DrawBalls : IBall
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private LogicAbstractAPI api;
        private float X;
        private float Y;
        //public ObservableCollection<BallToDraw> ballsToDraw = new ObservableCollection<BallToDraw>();

        public LogicAbstractAPI Api { get => api; set => api = value; }

        public float x {
            get { return X; } 
            private set
            {
                if (X != value)
                {
                    X = value;
                    RaisePropertyChanged();
                }
            } 
        }

        public float y {
            get { return Y; }
            private set
            {
                if (Y != value)
                {
                    Y = value;
                    RaisePropertyChanged();
                }
            }
        }

        public float r { get; internal set; }

        public DrawBalls(float xpos, float ypos)
        {
            this.X = xpos;
            this.Y = ypos;
        }

        /*        public DrawBalls(LogicAbstractAPI api) {
                    this.api = (IObservable<LogicAbstractAPI>)api;
                }*/

        /*        public DrawBalls(LogicAbstractAPI api)
                {
                    this.api = api;
                    //this.GetBallsToDraw();
                }*/
        /*        public LogicAbstractAPI getAPI()
                {
                    return api;
                }*/


        /*        public void GetBallsToDraw() {
                    for (int i = 0; i < getCoordinates().Length; i++) {
                        //Debug.WriteLine("hej");
                        //Debug.WriteLine(getCoordinates()[i][0] + " " + getCoordinates()[i][1]);
                        var ball = new BallToDraw(getCoordinates()[i][0], getCoordinates()[i][1]);
                        Debug.WriteLine(ball.X + " " + ball.Y);

                        this.ballsToDraw.Add(ball);
                    }

                }*/

/*        public float[][] getCoordinates()
        {
            //this.RaisePropertyChanged("a");
            return getAPI().getCoordinates();
        }*/


        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
