using ObligatorioTT.Services;
using ObligatorioTT.ViewModels;
using System.Xml;
namespace ObligatorioTT.Pages;

public partial class Login : ContentPage
{

    public Login( )
    {
        InitializeComponent();
    }

    private async void btnInicioSesion_Clicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }




    private async void On_Tapped(object sender, EventArgs e)
    {
        Registrarme.TextColor = Colors.White; // Cambia el color al hacer tap
        await Navigation.PushAsync(new CrearPerfil());
    }

    private async void Foto_Clicked(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                await DisplayAlert("Genial", "Ya sacaste la foto", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("ERROR", "No se pudo conectar con la camara", "Cerrar");


        }


    }
    protected override async void OnDisappearing()
    {
        base.OnDisappearing();

        // Aplicar animación al desaparecer
        await this.ScaleTo(0.5, 250, Easing.CubicOut);
    }
}