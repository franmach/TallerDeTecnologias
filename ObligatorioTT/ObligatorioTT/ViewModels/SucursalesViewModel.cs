using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ObligatorioTT.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ObligatorioTT.Pages;

namespace ObligatorioTT.ViewModels
{
    public partial class SucursalesViewModel : ObservableObject
    {
        private readonly Repository _repository;

        public SucursalesViewModel(Repository repository)
        {
            _repository = repository;
            Sucursales = new ObservableCollection<Sucursal>();
        }

        // Propiedad observable para el estado de carga
        [ObservableProperty]
        private bool isBusy;

        // Propiedad observable para la visibilidad de las sucursales
        [ObservableProperty]
        private bool isSucursalesVisible = false;

        // Colección observable para la lista de sucursales
        public ObservableCollection<Sucursal> Sucursales { get; }

        // Comando para cargar las sucursales
        [RelayCommand]
        public async Task LoadSucursalesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var sucursales = await _repository.GetAllSucursalesAsync();
                Sucursales.Clear();

                foreach (var sucursal in sucursales)
                {
                    Sucursales.Add(sucursal);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Comando para eliminar una sucursal
        [RelayCommand]
        public async Task DeleteSucursalAsync(Sucursal sucursal)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                await _repository.DeleteSucursalAsync(sucursal.id);
                Sucursales.Remove(sucursal);
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Comando para navegar a la página CrearSucursal
        [RelayCommand]
        private async Task NavigateToCrearSucursalAsync()
        {
            await Shell.Current.GoToAsync("CrearSucursales");
        }

        // Comando para alternar la visibilidad de las sucursales
        [RelayCommand]
        private void ToggleSucursalesVisibility()
        {
            IsSucursalesVisible = !IsSucursalesVisible;
        }
    }
}


//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using ObligatorioTT.Models;
//using System.Collections.ObjectModel;
//using System.Threading.Tasks;
//using ObligatorioTT.Pages;


//namespace ObligatorioTT.ViewModels
//{
//    public partial class SucursalesViewModel : ObservableObject
//    {
//        private readonly Repository _repository;

//        public SucursalesViewModel(Repository repository)
//        {
//            _repository = repository;
//            Sucursales = new ObservableCollection<Sucursal>();
//        }

//        // Propiedad observable para el estado de carga
//        [ObservableProperty]
//        private bool isBusy;

//        // Colección observable para la lista de sucursales
//        public ObservableCollection<Sucursal> Sucursales { get; }

//        // Comando para cargar las sucursales
//        [RelayCommand]
//        public async Task LoadSucursalesAsync()
//        {
//            if (IsBusy)
//                return;

//            try
//            {
//                IsBusy = true;
//                var sucursales = await _repository.GetAllSucursalesAsync();
//                Sucursales.Clear();

//                foreach (var sucursal in sucursales)
//                {
//                    Sucursales.Add(sucursal);
//                }
//            }
//            finally
//            {
//                IsBusy = false;
//            }
//        }

//        // Comando para eliminar una sucursal
//        [RelayCommand]
//        public async Task DeleteSucursalAsync(Sucursal sucursal)
//        {
//            if (IsBusy)
//                return;

//            try
//            {
//                IsBusy = true;
//                await _repository.DeleteSucursalAsync(sucursal.id);
//                Sucursales.Remove(sucursal);
//            }
//            finally
//            {
//                IsBusy = false;
//            }
//        }

//        [RelayCommand]
//        private async Task NavigateToCrearSucursalAsync()
//        {
//            await Shell.Current.GoToAsync("CrearSucursales");

//        }

//    }
//}
