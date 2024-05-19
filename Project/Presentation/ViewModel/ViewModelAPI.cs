using Data;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace ViewModel
{
    //this class has to stay public, so that MainWindow.xaml can access it
    public class ViewModel
    {
        public ICommand startSimulation { get; set; }
        public ICommand stopSimulation { get; set; }
        private ModelAbstractAPI api;
        public int amount;

        public ObservableCollection<Model.IBall> ballsToDraw { get; } = new ObservableCollection<Model.IBall>();
        
        public ViewModel()
        {
            //DI is not really doable here
            startSimulation = new RelayCommand(startSimulationHandler);
            stopSimulation = new RelayCommand(stopSimulationHandler);
        }

        private void startSimulationHandler(object obj)
        {
            api = ModelAbstractAPI.CreateModelAPI();
            getBoardParameters(700, 300, amount);
            IDisposable observer = api.Subscribe(x => ballsToDraw.Add(x)); //look at ModelAPI.cs@89
            foreach (Model.IBall b in api.getballs())
            {
                ballsToDraw.Add(b);
            }
            api.StartSimulation();
        }

        private void getBoardParameters(int x, int y, int ballsAmount) {
            api.getBoardParameters(x, y, ballsAmount);
        }

        private void stopSimulationHandler(object obj)
        {
            api.StopSimulation();
            ballsToDraw.Clear();
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
