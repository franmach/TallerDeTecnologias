using ObligatorioTT.Models;
using ObligatorioTT.ViewModels;
using System;

namespace ObligatorioTT.Pages
{
    public partial class Perfil : ContentPage
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly Repository _repository;

        public Perfil(HomeViewModel homeViewModel, Repository repository)
        {
            InitializeComponent();
            _homeViewModel = homeViewModel;
            _repository = repository;
            BindingContext = _homeViewModel;
        }

        private async void btnAgregarCliente_Clicked(object sender, EventArgs e)
        {
            try
            {
                string nombre = Nombre.Text;
                string telefono = Telefono.Text;
                string email = Email.Text;
                string password = Password.Text;
                string rutaFoto = RutaFoto.Text;

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    statusMessage.Text = "Por favor, complete todos los campos obligatorios.";
                    return;
                }

                Cliente nuevoCliente = new()
                {
                    nombre = nombre,
                    telefono = telefono,
                    email = email,
                    password = password,
                    rutaFoto = rutaFoto
                };

                await _repository.AddNewClienteAsync(nuevoCliente);

                statusMessage.Text = "Cliente agregado correctamente";
                Nombre.Text = Telefono.Text = Email.Text = Password.Text = RutaFoto.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in btnAgregarCliente_Clicked: {ex.Message}");
                statusMessage.Text = "Error al agregar cliente";
            }
        }
    }
}
