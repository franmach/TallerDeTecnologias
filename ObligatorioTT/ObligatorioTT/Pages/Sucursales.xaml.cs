using ObligatorioTT.Models;
using ObligatorioTT.ViewModels;

namespace ObligatorioTT.Pages;

public partial class Sucursales : ContentPage
{
    private readonly SucursalesViewModel _viewModel;

    public Sucursales(SucursalesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadSucursalesCommand.Execute(null);
    }
}
