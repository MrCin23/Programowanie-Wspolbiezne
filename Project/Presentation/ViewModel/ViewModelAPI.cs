﻿using Model;
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
        private ModelAbstractAPI api;
        public int amount;
        public ObservableCollection<IBall> ballsToDraw { get; } = new ObservableCollection<IBall>();

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
            api = ModelAbstractAPI.CreateModelAPI(chooseBallAmount);
            IDisposable observer = api.Subscribe<IBall>(x => ballsToDraw.Add(x));
            //MessageBox.Show(chooseBallAmount.ToString());
            //Debug.WriteLine("abc");
            /*            Thread thread = new Thread(() =>
                        {*/
            api.StartSimulation();
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
            api.StopSimulation();
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


/*        public float[][] getCoordinates()
        {
            return model.getCoordinates();
        }*/ 
    }
}
