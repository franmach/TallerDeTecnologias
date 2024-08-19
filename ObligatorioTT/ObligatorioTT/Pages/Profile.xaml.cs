using Microsoft.Maui.ApplicationModel.Communication;
using ObligatorioTT.Models;
using ObligatorioTT.Services;
using ObligatorioTT.Pages;

namespace ObligatorioTT.Pages;

    public partial class Profile : ContentPage
    {
        private readonly Repository _repository;
        private Usuario _usuario;

        public Profile()
        {
            InitializeComponent();
            _repository = App.ObligatorioRepo;
            CargarPerfilUsuario();
        }

        private void CargarPerfilUsuario()
        {
            _usuario = App.UsuarioLogueado;

            if (_usuario != null)
            {
                BindingContext = _usuario;
            }
            else
            {
                // Manejar el caso donde no hay usuario logueado
                DisplayAlert("Error", "No hay un usuario logueado", "OK");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            // Limpiar la sesión del usuario
            App.UsuarioLogueado = null;

        // Redirigir a la página de inicio de sesión
            await Shell.Current.GoToAsync("//Login");


    }

    private async void btnNuevoPerfil_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CrearPerfil());

    }
}


