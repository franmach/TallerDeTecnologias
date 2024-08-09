using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ObligatorioTT.Pages;
using ObligatorioTT.Services;
using ObligatorioTT.ViewModels;

namespace ObligatorioTT
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();

            builder.Services.AddTransient<AuthService>();
            builder.Services.AddTransient<Loading>();
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<Profile>();





#endif
            builder.Services.AddHttpClient(TmdbService.TmdbHttpClientName,
                httpClient => httpClient.BaseAddress = new Uri("https://api.themoviedb.org"));

            builder.Services.AddSingleton<TmdbService>(); //se registra el servicio
            builder.Services.AddSingleton<MainPage>();//Usamos el patron Singleton para garantizar que solamente haya una instancia de TMDBService en toda la vida de la app, cada vez que se llame se devuelve la misma instnacia
            builder.Services.AddSingleton<HomeViewModel>();
            // Añadir Repository como singleton
            string rutaDB = FileAccessHelper.GetLocalFilePath("usuariosdb.db3");
            builder.Services.AddSingleton<Repository>(s => ActivatorUtilities.CreateInstance<Repository>(s, rutaDB));

            return builder.Build();
        }
    }
}
