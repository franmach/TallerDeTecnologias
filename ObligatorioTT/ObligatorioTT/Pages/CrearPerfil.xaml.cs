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
                string rutaFoto = Foto.Text;  // Get the file path from Foto.Text

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("", "Por favor, complete todos los campos obligatorios.","Cerrar");
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
               await DisplayAlert("", "Usuario agregado correctamente", "Cerrar");

                Nombre.Text = Telefono.Text = Email.Text = Password.Text = Foto.Text =  string.Empty;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in btnAgregarUsuario_Clicked: {ex.Message}");
               await DisplayAlert("", "Error al agregar usuario", "Cerrar");
            }
        }
    


        private async void Foto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null)
                {
                    var filePath = Path.Combine(FileSystem.AppDataDirectory, $"{Guid.NewGuid()}.jpg");
                    using var stream = await photo.OpenReadAsync();
                    using var fileStream = File.OpenWrite(filePath);
                    await stream.CopyToAsync(fileStream);

                    Foto.Text = filePath; // Store the file path in the button's text
                    await DisplayAlert("Genial", "Foto tomada y guardada", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERROR", "No se pudo conectar con la cámara", "Cerrar");
            }
        }

    }
}