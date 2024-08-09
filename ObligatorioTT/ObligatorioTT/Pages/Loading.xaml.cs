using ObligatorioTT.Services;
namespace ObligatorioTT.Pages;

public partial class Loading : ContentPage
{
	private readonly AuthService _authService;

    public Loading(AuthService authService)
	{
		InitializeComponent();

        _authService = authService;
	}

	protected async override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		if (await _authService.IsAuthenticatedAsync())
		{
            //usuario logeado
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        }
        else
		{
			//usuario no logeado
			await Shell.Current.GoToAsync($"//{nameof(Login)}");
		}
	}
}