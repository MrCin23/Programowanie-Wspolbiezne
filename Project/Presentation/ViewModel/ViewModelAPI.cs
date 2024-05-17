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
    public class ViewModel : INotifyPropertyChanged
    {
        public ICommand startSimulation { get; set; }
        public ICommand stopSimulation { get; set; }
        private ModelAbstractAPI api;
        public int amount;

        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Model.IBall> ballsToDraw { get; } = new ObservableCollection<Model.IBall>();
        
        public ViewModel()
        {
            //DI is not really doable here
            startSimulation = new RelayCommand(startSimulationHandler);
            stopSimulation = new RelayCommand(stopSimulationHandler);
        }

        protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //Debug.WriteLine(ballsToDraw[0].x + " " + ballsToDraw[0].y);
        }

        //todo
#nullable enable
        public event EventHandler<ModelEventArgs>? ChangedPosition;

        private void OnPropertyChanged(ModelEventArgs args)
        {
            ChangedPosition?.Invoke(this, args);
        }
        private void sendUpdate(object sender, ModelEventArgs e)
        {
            Model.IBall ball = (Model.IBall)sender;
            Vector2 pos = ball.pos;
            ModelEventArgs args = new ModelEventArgs(pos);
            OnPropertyChanged(args);
            RaisePropertyChanged(nameof(pos));
        }

        private void startSimulationHandler(object obj)
        {
            api = ModelAbstractAPI.CreateModelAPI();
            getBoardParameters(700, 300, amount);
            IDisposable observer = api.Subscribe(x => ballsToDraw.Add(x)); //look at ModelAPI.cs@89
            foreach (Model.IBall b in api.getballs())
            {
                ballsToDraw.Add(b);
/*                Vector2 pos = b.pos;
                ModelEventArgs args = new ModelEventArgs(pos);*/
                b.ChangedPosition += sendUpdate;
                b.PropertyChanged += RaisePropertyChanged;

            }
            api.StartSimulation();
            RaisePropertyChanged("Circles");
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
