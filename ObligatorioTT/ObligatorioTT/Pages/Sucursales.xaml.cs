using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using ObligatorioTT.Models;
using ObligatorioTT.ViewModels;

namespace ObligatorioTT.Pages;

public partial class Sucursales : ContentPage
{
    private readonly SucursalesViewModel _viewModel;

    public Sucursales(SucursalesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;

        MessagingCenter.Subscribe<CrearSucursales, Location>(this, "NuevaSucursalAgregada", (sender, posicion) =>
        {
            var nuevoPin = new Pin
            {
                Label = "Nueva Sucursal",
                Location = new Location(posicion.Latitude, posicion.Longitude),
                Address = "Dirección ingresada"
            };

            SucursalesMap.Pins.Add(nuevoPin);
        });

        MessagingCenter.Subscribe<CrearSucursales>(this, "SucursalAgregada", (sender) =>
        {
            _viewModel.LoadSucursalesCommand.Execute(null);
        });

        MessagingCenter.Subscribe<SucursalesViewModel, Location>(this, "SucursalEliminada", (sender, posicion) =>
        {
            var pinToRemove = SucursalesMap.Pins.FirstOrDefault(pin =>
                pin.Location.Latitude == posicion.Latitude && pin.Location.Longitude == posicion.Longitude);

            if (pinToRemove != null)
            {
                SucursalesMap.Pins.Remove(pinToRemove);
                Console.WriteLine($"Pin eliminado en la ubicación: {posicion.Latitude}, {posicion.Longitude}");
            }
            else
            {
                Console.WriteLine($"No se encontró pin para eliminar en la ubicación: {posicion.Latitude}, {posicion.Longitude}");
            }
        });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadSucursalesCommand.Execute(null);

        var geolocalizacion = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(1));
        var ubicacion = await Geolocation.GetLocationAsync(geolocalizacion);

        SucursalesMap.MoveToRegion(MapSpan.FromCenterAndRadius(ubicacion, Distance.FromKilometers(2)));
    }


}
