using ObligatorioTT.Models;

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

            Sucursal nuevaSucursal = new()
            {
                nombre = nombre,
                direccion = direccion,
                telefono = telefono
            };

            await _repository.AddNewSucursalAsync(nuevaSucursal);

            statusMessage.Text = "Sucursal agregada correctamente";
            Nombre.Text = Direccion.Text = Telefono.Text = string.Empty;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in btnAgregarUsuario_Clicked: {ex.Message}");
            statusMessage.Text = "Error al agregar sucursal";
        }
    }


}