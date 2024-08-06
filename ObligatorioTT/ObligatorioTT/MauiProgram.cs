﻿using CommunityToolkit.Maui;
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
#endif
            builder.Services.AddHttpClient(TmdbService.TmdbHttpClientName,
                httpClient => httpClient.BaseAddress = new Uri("https://api.themoviedb.org"));

            builder.Services.AddSingleton<TmdbService>(); //se registra el servicio
            builder.Services.AddSingleton<MainPage>();//Usamos el patron Singleton para garantizar que solamente haya una instancia de TMDBService en toda la vida de la app, cada vez que se llame se devuelve la misma instnacia
            builder.Services.AddSingleton<HomeViewModel>();


            return builder.Build();
        }
    }
}