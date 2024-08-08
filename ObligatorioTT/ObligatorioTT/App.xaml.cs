namespace ObligatorioTT
{
    public partial class App : Application
    {
        // TODO: Add a public static PersonRepository property
        public static Repository ObligatorioRepo { get; set; }
        public App(Repository repositorio)
        {
            InitializeComponent();

            MainPage = new AppShell();

            // TODO: Initialize the PersonRepository property with the PersonRespository singleton object
            ObligatorioRepo = repositorio;
        }

    }
}
