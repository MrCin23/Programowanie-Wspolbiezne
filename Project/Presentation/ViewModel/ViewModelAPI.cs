﻿using Data;
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
    public class ViewModel : INotifyPropertyChanged
    {
        public ICommand startSimulation { get; set; }
        public ICommand stopSimulation { get; set; }
        private ModelAbstractAPI api;
        public int amount;
        public ObservableCollection<IBall> ballsToDraw { get; } = new ObservableCollection<IBall>();
        
        public ViewModel()
        {
            //DI is not really doable here
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
            api = ModelAbstractAPI.CreateModelAPI(700, 300, chooseBallAmount);
            IDisposable observer = api.Subscribe<IBall>(x => ballsToDraw.Add(x)); //look at ModelAPI.cs@89
            foreach (IBall b in api.getballs())
            {
                ballsToDraw.Add(b);
            }
            api.StartSimulation();
        }

        private void stopSimulationHandler(object obj)
        {
            api.StopSimulation();
        }

        public int chooseBallAmount
        {
            get { return amount; }
            set
            {
                amount = value;
            }
        }
    }
}
