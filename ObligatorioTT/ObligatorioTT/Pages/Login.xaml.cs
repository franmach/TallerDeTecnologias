using ObligatorioTT.Services;
using ObligatorioTT.ViewModels;
namespace ObligatorioTT.Pages;

public partial class Login : ContentPage
{
    private readonly AuthService _authService;

    public Login(AuthService authService)
    {
        InitializeComponent();
        _authService = authService;
    }

    private async void btnInicioSesion_Clicked(object sender, EventArgs e)
    {
        _authService.Login();

        await Shell.Current.GoToAsync($"//{nameof(Loading)}");
    }
    private async  void On_Tapped(object sender, TappedEventArgs e)
    {

    }
}