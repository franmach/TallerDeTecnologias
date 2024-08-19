using ObligatorioTT.Models;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Devices.Sensors;



namespace ObligatorioTT.Pages;

public partial class CrearSucursales : ContentPage
{
    public readonly Repository _repository;
    public CrearSucursales(Repository repository)
    {
        InitializeComponent();
        _repository = repository;
        BindingContext = _repository;
    }
    public CrearSucursales() : this(App.ObligatorioRepo)
    {
    }

    private async void btnAgregarSucursal_Clicked(object sender, EventArgs e)
    {
        try
        {
            string nombre = Nombre.Text;
            string direccion = Direccion.Text;
            string telefono = Telefono.Text;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(direccion))
            {
                statusMessage.Text = "Por favor, complete todos los campos obligatorios.";
                return;
            }

            // Usa el servicio de geocodificación
            var geocodingService = new GeocodingService("AIzaSyB6mykcb3pshOlbdIaUpYtOmLK-fdkXsW0"); // Reemplaza con tu clave de API
            var posicion = await geocodingService.GetCoordinatesFromAddressAsync(direccion);

            if (posicion == null)
            {
                await DisplayAlert("Error", "No se pudo encontrar la ubicación de la dirección ingresada.", "OK");
                return;
            }

            Sucursal nuevaSucursal = new()
            {
                nombre = nombre,
                direccion = direccion,
                telefono = telefono
            };

            // Guardar la nueva sucursal en la base de datos
            await _repository.AddNewSucursalAsync(nuevaSucursal);

            // Enviar mensaje para recargar la lista de sucursales
            MessagingCenter.Send(this, "SucursalAgregada");

            // Enviar mensaje con la nueva posición para agregar el pin
            MessagingCenter.Send(this, "NuevaSucursalAgregada", posicion);

            await DisplayAlert("Éxito", "Sucursal agregada correctamente", "OK");

            statusMessage.Text = "Sucursal agregada correctamente";
            Nombre.Text = Direccion.Text = Telefono.Text = string.Empty;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en btnAgregarSucursal_Clicked: {ex.Message}");
            await DisplayAlert("Error", $"Error al agregar sucursal: {ex.Message}", "OK");
        }
    }





    //private async void btnAgregarSucursal_Clicked(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string nombre = Nombre.Text;
    //        string direccion = Direccion.Text;
    //        string telefono = Telefono.Text;


    //        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(direccion))
    //        {
    //            statusMessage.Text = "Por favor, complete todos los campos obligatorios.";
    //            return;
    //        }

    //        Sucursal nuevaSucursal = new()
    //        {
    //            nombre = nombre,
    //            direccion = direccion,
    //            telefono = telefono
    //        };

    //        await _repository.AddNewSucursalAsync(nuevaSucursal);
    //        await DisplayAlert("Éxito", "Sucursal agregada correctamente", "OK");

    //        statusMessage.Text = "Sucursal agregada correctamente";
    //        Nombre.Text = Direccion.Text = Telefono.Text = string.Empty;
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"Error in btnAgregarUsuario_Clicked: {ex.Message}");
    //        await DisplayAlert("Error", "Error al agregar sucursal", "OK");
    //    }
    //}

    private async void btnVerSucursales_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Sucursales");

    }
}