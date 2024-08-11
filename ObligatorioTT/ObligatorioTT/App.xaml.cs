using ObligatorioTT.Pages;

namespace ObligatorioTT
{
    public partial class App : Application
    {
        // TODO: Add a public static PersonRepository property
        public static Repository ObligatorioRepo { get; set; }
        public App(Repository repositorio)
        {
            InitializeComponent();
            //Page a = new AppShell();
            //MainPage = new Login();

            MainPage = new AppShell();

            // TODO: Initialize the PersonRepository property with the PersonRespository singleton object
            ObligatorioRepo = repositorio;
        }

    }
}