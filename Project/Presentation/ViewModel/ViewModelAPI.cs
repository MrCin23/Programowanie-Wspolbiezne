using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace ViewModel
{
/*    public abstract class ViewModelAbstractAPI
    {

        public static ViewModelAbstractAPI CreateViewModelAPI(int x, int y, int amount)
        {
            return new ViewModel(ModelAbstractAPI.CreateModelAPI(x, y, amount));
        }
    }*/

    public class ViewModel : INotifyPropertyChanged
    {
        public ICommand startSimulation { get; set; }
        public ICommand stopSimulation { get; set; }
        private ModelAbstractAPI model;
        public int amount;
        private static readonly ObservableCollection<BallToDraw> ballToDraws;
        public Object[] data;

        public ViewModel()
        {
            startSimulation = new RelayCommand(startSimulationHandler);
            stopSimulation = new RelayCommand(stopSimulationHandler);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void startSimulationHandler(object obj)
        {

            //MessageBox.Show(chooseBallAmount.ToString());
            //Debug.WriteLine("abc");
            model = ModelAbstractAPI.CreateModelAPI(700, 300, chooseBallAmount);
/*            Thread thread = new Thread(() =>
            {*/
            model.startSimulation();
            data = model.getData();
/*        });
            thread.IsBackground = true;
            thread.Start();
            foreach (var b in model.drawBalls)
            {
                test(b);
            }*/
        }
        private void stopSimulationHandler(object obj)
        {
            //MessageBox.Show("a");
            model.stopSimulation();
        }

        public int chooseBallAmount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged("Ball Amount");
            }
        }


        public float[][] getCoordinates()
        {
            return model.getCoordinates();
        }

        public void test(Object ball)
        {
            while (true)
            {
                Debug.WriteLine(ball.x + " " + ball.y);
            }
        }

        public ObservableCollection<BallToDraw> ballsToDraw { get; set; } = ballToDraws;
    }
}
