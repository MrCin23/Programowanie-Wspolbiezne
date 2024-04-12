using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
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
            
            MessageBox.Show(chooseBallAmount.ToString());
            model = ModelAbstractAPI.CreateModelAPI(700, 300, 9);
            //model.startSimulation();
        }
        private void stopSimulationHandler(object obj)
        {
            MessageBox.Show("a");
            //model.stopSimulation();
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
    }
    /*public class BilliardsViewModel : INotifyPropertyChanged
    {
        private int _chooseBallAmount;
        private ModelAbstractAPI model;

        public int ChooseBallAmount
        {
            get { return _chooseBallAmount; }
            set
            {
                _chooseBallAmount = value;
                OnPropertyChanged(nameof(ChooseBallAmount));
            }
        }

        public ICommand startSimulation { get; }
        public ICommand stopSimulation { get; }

        public BilliardsViewModel(ModelAbstractAPI api)
        {

        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }*/


}
