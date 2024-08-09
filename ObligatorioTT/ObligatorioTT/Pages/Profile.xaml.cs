using ObligatorioTT.Services;

namespace ObligatorioTT.Pages;

public partial class Profile : ContentPage
{
    private readonly AuthService _authService;

    public Profile(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        _authService.Logout();
        Shell.Current.GoToAsync($"//{nameof(Login)}");

    }

   
}