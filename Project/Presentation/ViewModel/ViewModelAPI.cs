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
    //this class has to stay public, so that MainWindow.xaml can access it
    public class ViewModel : INotifyPropertyChanged
    {
        public ICommand startSimulation { get; set; }
        public ICommand stopSimulation { get; set; }
        private ModelAbstractAPI api;
        public int amount;
        private Task task;

        public ObservableCollection<Model.IBall> ballsToDraw { get; } = new ObservableCollection<Model.IBall>();
        
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
            api = ModelAbstractAPI.CreateModelAPI();
            getBoardParameters(700, 300, amount);
            IDisposable observer = api.Subscribe<Model.IBall>(x => ballsToDraw.Add(x)); //look at ModelAPI.cs@89
            foreach (Model.IBall b in api.getballs())
            {
                ballsToDraw.Add(b);
            }
            task = new Task(() =>
            {
                api.StartSimulation();
            });
            task.Start();
        }

        private void getBoardParameters(int x, int y, int ballsAmount) {
            api.getBoardParameters(x, y, ballsAmount);
        }

        private void stopSimulationHandler(object obj)
        {
            api.StopSimulation();
            ballsToDraw.Clear();
            task.Dispose();
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
