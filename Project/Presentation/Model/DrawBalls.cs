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

    public class BallToDraw
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
    }

    public class DrawBalls : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private LogicAbstractAPI api;
        public ObservableCollection<BallToDraw> ballsToDraw = new ObservableCollection<BallToDraw>();

        public LogicAbstractAPI Api { get => api; set => api = value; }
        
        /*        public DrawBalls(LogicAbstractAPI api) {
                    this.api = (IObservable<LogicAbstractAPI>)api;
                }*/

        public DrawBalls(LogicAbstractAPI api)
        {
            this.api = api;
            this.GetBallsToDraw();
        }
        public LogicAbstractAPI getAPI()
        {
            return api;
        }


        public void GetBallsToDraw() {
            for (int i = 0; i < getCoordinates().Length; i++) {
                //Debug.WriteLine("hej");
                //Debug.WriteLine(getCoordinates()[i][0] + " " + getCoordinates()[i][1]);
                var ball = new BallToDraw(getCoordinates()[i][0], getCoordinates()[i][1]);
                Debug.WriteLine(ball.X + " " + ball.Y);

                this.ballsToDraw.Add(ball);
            }

        }

        public float[][] getCoordinates()
        {
            //this.RaisePropertyChanged("a");
            return getAPI().getCoordinates();
        }
    }
}
