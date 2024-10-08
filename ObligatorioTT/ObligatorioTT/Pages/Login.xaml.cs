using ObligatorioTT.Services;
using ObligatorioTT.ViewModels;
using System.Xml;
using ObligatorioTT.Models;
using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;
using Microsoft.Maui.Controls;
using System.Runtime.Intrinsics.X86;
using System.ComponentModel;
namespace ObligatorioTT.Pages;

public partial class Login : ContentPage
{
    private readonly Repository _repository;

    public Login(Repository repository)
    {
        InitializeComponent();
        _repository = repository;
        BindingContext = _repository;

        // Configurar la interfaz seg�n la plataforma
        ConfigurarInterfazPorPlataforma();
    }

    private void ConfigurarInterfazPorPlataforma()
    {
        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            // Ocultar el bot�n de huella en Windows
            btnHuella.IsVisible = false;
        }
        else if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            // Mostrar el bot�n de huella en Android
            btnHuella.IsVisible = true;
        }
    }

    private async void btnInicioSesion_Clicked(object sender, EventArgs e)
    {
        List<Usuario> usuarios = await App.ObligatorioRepo.GetAllUsuarios();
        bool usuarioEncontrado = false;

        foreach (var u in usuarios)
        {
            if (u.email == email.Text && u.password == password.Text)
            {
                // Usuario logeado
                await Shell.Current.GoToAsync($"{nameof(Loading)}");
                usuarioEncontrado = true;
                App.UsuarioLogueado = u;

                break; // Salir del ciclo al encontrar un usuario v�lido
            }
        }

        if (!usuarioEncontrado)
        {
            await DisplayAlert("Error", "Usuario y/o contrase�a invalidos", "Cerrar");
            // Usuario no encontrado
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }

    private async void On_Tapped(object sender, EventArgs e)
    {
        Registrarme.TextColor = Colors.White; // Cambia el color al hacer tap
        await Shell.Current.GoToAsync($"//{nameof(CrearPerfil)}");
    }

    private async void btnHuella_Clicked(object sender, EventArgs e)
    {
        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            await DisplayAlert("", "Esta plataforma no soporta el uso de huella dactilar", "Cerrar");
        }
        else if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            var request = new AuthenticationRequestConfiguration("Hyella", "huella dactilar");

            var result = await CrossFingerprint.Current.AuthenticateAsync(request);

            if (result.Authenticated)
            {
                await DisplayAlert("Autenticacion", "Acceso correcto", "Cerrar");
                await Shell.Current.GoToAsync($"{nameof(Loading)}"); // Navega a la siguiente p�gina
            }
            else
            {
                await DisplayAlert("Error en la autenticacion", "Acceso denegado", "Cerrar");
            }
        }
    }
}


//public partial class Login : ContentPage
//{
//    private readonly Repository _repository;

//    public Login(Repository repository)
//    {
//        InitializeComponent();
//        _repository = repository;
//        BindingContext = _repository;

//    }

//    private async void btnInicioSesion_Clicked(object sender, EventArgs e)
//    {
//        List<Usuario> usuarios = await App.ObligatorioRepo.GetAllUsuarios();
//        bool usuarioEncontrado = false;

//        foreach (var u in usuarios)
//        {
//            if (u.email == userEmail.Text)
//            {
//                Usuario logeado
//                await Shell.Current.GoToAsync($"{nameof(Loading)}");
//                usuarioEncontrado = true;
//                break; // Salir del ciclo al encontrar un usuario v�lido
//            }
//        }

//        if (!usuarioEncontrado)
//        {
//            await DisplayAlert("Error", "Usuario o contrase�a invalidos", "Cerrar");
//            Usuario no encontrado
//           await Shell.Current.GoToAsync($"//{nameof(Login)}");
//        }
//    }





//    private async void On_Tapped(object sender, EventArgs e)
//    {
//        Registrarme.TextColor = Colors.White; // Cambia el color al hacer tap
//        await Shell.Current.GoToAsync($"//{nameof(CrearPerfil)}");
//    }



//    private async void btnHuella_Clicked(object sender, EventArgs e)
//    {
//        if (DeviceInfo.Platform == DevicePlatform.WinUI)
//        {
//            await DisplayAlert("Plataforma", "Esta plataforma no soporta el uso de huella dactilar", "Cerrar");
//        }
//        else
//        {

//            var request = new AuthenticationRequestConfiguration("demo", "probando huella");

//            var result = await CrossFingerprint.Current.AuthenticateAsync(request);

//            if (result.Authenticated)
//            {
//                await DisplayAlert("Autenticacion", "Acceso correcto", "Cerrar");
//            }
//            else
//            {
//                await DisplayAlert("Error en la autenticacion", "Acceso denegado", "Cerrar");
//            }
//        }
//    }
//}