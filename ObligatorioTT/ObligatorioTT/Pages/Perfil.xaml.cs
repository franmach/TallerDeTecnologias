using ObligatorioTT.ViewModels;
using System;
namespace ObligatorioTT.Pages;

public partial class Perfil : ContentPage
{
    private readonly HomeViewModel _homeViewModel;
    public Perfil(HomeViewModel homeViewModel)
	{
		InitializeComponent();
        _homeViewModel = homeViewModel;
        BindingContext = _homeViewModel;
    }

    private void btnAgregarUsuario_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Lógica para agregar un nuevo usuario
            string nombre = NuevoUsuario.Text;
            if (!string.IsNullOrEmpty(nombre))
            {
                // Agregar usuario a la base de datos o lista
                statusMessage.Text = "Usuario agregado correctamente";
            }
            else
            {
                statusMessage.Text = "Por favor, ingrese un nombre";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in btnAgregarUsuario_Clicked: {ex.Message}");
            // Manejo de la excepción
            statusMessage.Text = "Error al agregar usuario";
        }
    }
}