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

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private bool isSucursalesVisible = false;

        public ObservableCollection<Sucursal> Sucursales { get; }

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

                    // Enviar mensaje para agregar pins al mapa
                    var posicion = await new GeocodingService("AIzaSyB6mykcb3pshOlbdIaUpYtOmLK-fdkXsW0").GetCoordinatesFromAddressAsync(sucursal.direccion);
                    if (posicion != null)
                    {
                        // Crear una instancia de SucursalPinData y enviar el mensaje
                        var sucursalPinData = new SucursalPinData
                        {
                            Sucursal = sucursal,
                            Posicion = posicion
                        };
                        MessagingCenter.Send(this, "SucursalAgregada", sucursalPinData);
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

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

                // Enviar mensaje para eliminar el pin, utilizando las coordenadas de la sucursal
                var posicion = await new GeocodingService("AIzaSyB6mykcb3pshOlbdIaUpYtOmLK-fdkXsW0").GetCoordinatesFromAddressAsync(sucursal.direccion);
                if (posicion != null)
                {
                    MessagingCenter.Send(this, "SucursalEliminada", posicion);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task NavigateToCrearSucursalAsync()
        {
            await Shell.Current.GoToAsync("CrearSucursales");
        }

        [RelayCommand]
        private void ToggleSucursalesVisibility()
        {
            IsSucursalesVisible = !IsSucursalesVisible;
        }
    }


}



