using ObligatorioTT.Pages;

namespace ObligatorioTT
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CategoriesPage), typeof(CategoriesPage));
            
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(Profile), typeof(Profile));
            Routing.RegisterRoute(nameof(Loading), typeof(Loading));
            Routing.RegisterRoute(nameof(CrearPerfil), typeof(CrearPerfil));



        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Shell.Current.GoToAsync($"///{nameof(Login)}");
        }

       
    }
}
