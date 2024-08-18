using ObligatorioTT.ViewModels;

namespace ObligatorioTT.Pages;

public partial class MainPage : ContentPage
{
    private readonly HomeViewModel _homeViewModel;

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

    //evento del menu de categorias
    private async void CategoriesMenu_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CategoriesPage));
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
}
