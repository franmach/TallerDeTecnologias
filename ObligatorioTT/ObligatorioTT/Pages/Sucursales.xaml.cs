using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using ObligatorioTT.Models;
using ObligatorioTT.ViewModels;
using Microsoft.Maui.Devices.Sensors;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ObligatorioTT.Pages;

public partial class Sucursales : ContentPage
{
    private readonly SucursalesViewModel _viewModel;
    private readonly DirectionsService _directionsService;

    public Sucursales(SucursalesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;

        _directionsService = new DirectionsService("AIzaSyB6mykcb3pshOlbdIaUpYtOmLK-fdkXsW0"); // Reemplaza con tu clave de API

        // Suscribir eventos al iniciar
        SuscribirEventos();
    }

    private void SuscribirEventos()
    {
        // Suscripción para agregar un nuevo pin cuando se agrega una sucursal
        MessagingCenter.Subscribe<SucursalesViewModel, Location>(this, "SucursalAgregada", (sender, posicion) =>
        {
            var nuevoPin = new Pin
            {
                Label = "Sucursal",
                Location = new Location(posicion.Latitude, posicion.Longitude),
                Address = "Dirección ingresada"
            };

            SucursalesMap.Pins.Add(nuevoPin);
            nuevoPin.MarkerClicked += (s, args) => OnPinClicked(nuevoPin);
        });

        // Suscripción para eliminar el pin cuando se elimina una sucursal
        MessagingCenter.Subscribe<SucursalesViewModel, Location>(this, "SucursalEliminada", (sender, posicion) =>
        {
            var pinToRemove = SucursalesMap.Pins.FirstOrDefault(pin =>
                pin.Location.Latitude == posicion.Latitude && pin.Location.Longitude == posicion.Longitude);

            if (pinToRemove != null)
            {
                SucursalesMap.Pins.Remove(pinToRemove);
                SucursalesMap.MapElements.Clear(); // Elimina la ruta si el pin se elimina
            }
        });

        // Eliminar la ruta si se hace clic fuera de un pin
        SucursalesMap.MapClicked += (s, e) =>
        {
            SucursalesMap.MapElements.Clear();
        };
    }

    private async void OnPinClicked(Pin pin)
    {
        var ubicacionDispositivo = await Geolocation.GetLastKnownLocationAsync();

        if (ubicacionDispositivo != null && pin.Location != null)
        {
            SucursalesMap.MapElements.Clear(); // Limpiar rutas anteriores
            var polyline = await _directionsService.GetRouteAsync(ubicacionDispositivo, pin.Location);
            SucursalesMap.MapElements.Add(polyline);
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadSucursalesAsync(); // Cargar sucursales y agregar pins al mapa

        var geolocalizacion = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(1));
        var ubicacion = await Geolocation.GetLocationAsync(geolocalizacion);

        SucursalesMap.MoveToRegion(MapSpan.FromCenterAndRadius(ubicacion, Distance.FromKilometers(2)));
    }
}




//using Microsoft.Maui.Controls.Maps;
//using Microsoft.Maui.Maps;
//using ObligatorioTT.Models;
//using ObligatorioTT.ViewModels;

//namespace ObligatorioTT.Pages;

//public partial class Sucursales : ContentPage
//{
//    private readonly SucursalesViewModel _viewModel;

//    public Sucursales(SucursalesViewModel viewModel)
//    {
//        InitializeComponent();
//        _viewModel = viewModel;
//        BindingContext = _viewModel;

//        MessagingCenter.Subscribe<CrearSucursales, Location>(this, "NuevaSucursalAgregada", (sender, posicion) =>
//        {
//            var nuevoPin = new Pin
//            {
//                Label = "Nueva Sucursal",
//                Location = new Location(posicion.Latitude, posicion.Longitude),
//                Address = "Dirección ingresada"
//            };

//            SucursalesMap.Pins.Add(nuevoPin);
//        });

//        MessagingCenter.Subscribe<CrearSucursales>(this, "SucursalAgregada", (sender) =>
//        {
//            _viewModel.LoadSucursalesCommand.Execute(null);
//        });

//        MessagingCenter.Subscribe<SucursalesViewModel, Location>(this, "SucursalEliminada", (sender, posicion) =>
//        {
//            var pinToRemove = SucursalesMap.Pins.FirstOrDefault(pin =>
//                pin.Location.Latitude == posicion.Latitude && pin.Location.Longitude == posicion.Longitude);

//            if (pinToRemove != null)
//            {
//                SucursalesMap.Pins.Remove(pinToRemove);
//                Console.WriteLine($"Pin eliminado en la ubicación: {posicion.Latitude}, {posicion.Longitude}");
//            }
//            else
//            {
//                Console.WriteLine($"No se encontró pin para eliminar en la ubicación: {posicion.Latitude}, {posicion.Longitude}");
//            }
//        });
//    }

//    protected override async void OnAppearing()
//    {
//        base.OnAppearing();
//        _viewModel.LoadSucursalesCommand.Execute(null);

//        var geolocalizacion = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(1));
//        var ubicacion = await Geolocation.GetLocationAsync(geolocalizacion);

//        SucursalesMap.MoveToRegion(MapSpan.FromCenterAndRadius(ubicacion, Distance.FromKilometers(2)));
//    }


//}
