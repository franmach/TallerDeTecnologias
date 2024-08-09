using ObligatorioTT.Models;
using ObligatorioTT.ViewModels;
using System;

namespace ObligatorioTT.Pages
{
    public partial class CrearPerfil : ContentPage
    {
        public readonly Repository _repository;

        public CrearPerfil(Repository repository)
        {
            InitializeComponent();
            _repository = repository;
            BindingContext = _repository;
        }

        public CrearPerfil() : this(App.ObligatorioRepo)
        {
        }
        private async void btnAgregarUsuario_Clicked(object sender, EventArgs e)
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

                Usuario nuevoUsuario = new()
                {
                    nombre = nombre,
                    telefono = telefono,
                    email = email,
                    password = password,
                    rutaFoto = rutaFoto
                };

                await _repository.AddNewUsuarioAsync(nuevoUsuario);

                statusMessage.Text = "Usuario agregado correctamente";
                Nombre.Text = Telefono.Text = Email.Text = Password.Text = RutaFoto.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in btnAgregarUsuario_Clicked: {ex.Message}");
                statusMessage.Text = "Error al agregar usuario";
            }
        }
    }
}