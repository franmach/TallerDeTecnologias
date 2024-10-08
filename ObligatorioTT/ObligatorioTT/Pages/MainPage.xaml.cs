
using ObligatorioTT.Pages;
using ObligatorioTT.ViewModels;
using ObligatorioTT.Services;

namespace ObligatorioTT.Pages;

public partial class MainPage : ContentPage
{
    private readonly HomeViewModel _homeViewModel;
    private readonly SearchViewModel _searchViewModel;

    public MainPage(HomeViewModel homeViewModel)
    {
        InitializeComponent();
        _homeViewModel = homeViewModel;
        BindingContext = _homeViewModel;       
    }

    //evento para controlar la seleccion de peliculas en la mainpage
    private void MovieRow_MediaSelected(object sender, Controls.MediaSelectEventsArgs e)
    {
        _homeViewModel.SelectMediaCommand.Execute(e.Media);
    }
    //evento para cerrar el infoBox
    private void MovieInfoBox_Closed(object sender, EventArgs e)
    {
        _homeViewModel.SelectMediaCommand.Execute(null);
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _homeViewModel.InitializeAsync();
    }
    private async Task AnimateLabel(Label label, bool isSelected)
    {
        if (isSelected)
        {
            await label.ScaleTo(1.5, 250);

        }
        else
        {
            await label.ScaleTo(1, 250);
        }
    }

    
   

    private async void UserButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Profile());

    }

   
}
