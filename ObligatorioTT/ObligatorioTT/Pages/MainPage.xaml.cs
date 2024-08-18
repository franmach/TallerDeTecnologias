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

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _homeViewModel.InitializeAsync();
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


    //private async void OnLabelTapped(object sender, EventArgs e)
    //{
    //    var selectedLabel = sender as Label;

    //    // Encontrar la posición del label seleccionado y del que está en el medio
    //    int selectedIndex = flexLayout.Children.IndexOf(selectedLabel);
    //    int middleIndex = flexLayout.Children.Count / 2;

    //    // Si el label seleccionado ya está en el medio, solo agrandarlo
    //    if (selectedIndex == middleIndex)
    //    {
    //        await AnimateLabel(selectedLabel, true);
    //    }
    //    else
    //    {
    //        // Resetear todos los labels a su estado original
    //        foreach (var child in flexLayout.Children)
    //        {
    //            if (child is Label label)
    //            {
    //                await AnimateLabel(label, false);
    //            }
    //        }

    //        // Intercambiar posiciones entre el label seleccionado y el del medio
    //        flexLayout.Children.Remove(selectedLabel);
    //        flexLayout.Children.Insert(middleIndex, selectedLabel);

    //        // Agrandar y resaltar el label seleccionado
    //        await AnimateLabel(selectedLabel, true);
    //    }

    //    // Navegar a una nueva página después de la animación
    //    //await Task.Delay(250); // Esperar a que termine la animación

    //    //// Aquí especificas la página a la que quieres navegar
    //    //await Navigation.PushAsync(new NextPage());
    //}

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
