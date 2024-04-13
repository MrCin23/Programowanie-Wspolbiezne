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
        public float x { get; set; }
        public float y { get; set; }
        public BallToDraw(float x, float y) {
            this.x = x; 
            this.y = y;
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
        }
        public LogicAbstractAPI getAPI()
        {
            return (LogicAbstractAPI)Api;
        }


        public void GetBallsToDraw() {
            for (int i = 0; i < getCoordinates().Length; i++) {
                //Debug.WriteLine(getCoordinates()[i][0] + " " + getCoordinates()[i][1]);
                var ball = new BallToDraw(getCoordinates()[i][0], getCoordinates()[i][1]);
                //Debug.WriteLine(ball.x + " " + ball.y);

                this.ballsToDraw.Add(ball);
            }

        }

        public float[][] getCoordinates()
        {
            this.RaisePropertyChanged("a");
            return getAPI().getCoordinates();
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
