using ObligatorioTT.ViewModels;

namespace ObligatorioTT.Pages;

public partial class FavoritesPage : ContentPage
{
    private readonly FavoritesViewModel _viewModel;

    //public FavoritesPage()
    //{
        
    //}

    public FavoritesPage(FavoritesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}