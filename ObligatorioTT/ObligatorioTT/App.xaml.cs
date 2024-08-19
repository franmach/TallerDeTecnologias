using ObligatorioTT.Pages;
using ObligatorioTT.Models;

namespace ObligatorioTT
{
    public partial class App : Application
    {
        public static Repository ObligatorioRepo { get; set; }
        public static Usuario UsuarioLogueado { get; set; }

        public App(Repository repositorio)
        {
            InitializeComponent();

            MainPage = new AppShell();

            ObligatorioRepo = repositorio;
        }

    }
}