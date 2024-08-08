namespace ObligatorioTT
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Pages.Perfil), typeof(Pages.Perfil));
        }
    }
}
