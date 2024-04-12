using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public abstract class ViewModelAbstractAPI
    {
        public static ViewModelAbstractAPI CreateViewModelAPI(int x, int y, int amount)
        {
            return new ViewModel(ModelAbstractAPI.CreateModelAPI(x, y, amount));
        }
    }

    internal class ViewModel : ViewModelAbstractAPI
    {
        public ICommand startSimulation { get; set; }
        public ICommand stopSimulation { get; set; }
        private ModelAbstractAPI model;
        public ViewModel(ModelAbstractAPI model) { 
            this.model = model;
            //startSimulation = new RelayCommand(() => ClickHandler());
            //stopSimulation = new RelayCommand(() => ExitClickHandler());
        }
        private void startSimulationHandler()
        {
            model.startSimulation();
        }
        private void stopSimulationHandler()
        {
            model.stopSimulation();
        }
        public float[][] getCoordinates()
        {
            return model.getCoordinates();
        }
    }
}
