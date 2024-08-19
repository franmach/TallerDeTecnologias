﻿using CommunityToolkit.Mvvm.ComponentModel;
using ObligatorioTT.Models;
using ObligatorioTT.Services;
using System.Collections.ObjectModel;


namespace ObligatorioTT.ViewModels
{
    [QueryProperty(nameof(Media), nameof(Media))]
    public partial class DetailsViewModel : ObservableObject
    {
        private readonly TmdbService _tmdbService;
        public DetailsViewModel(TmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        [ObservableProperty]
        private Media _media;

        [ObservableProperty]
        private string _mainTrailerUrl;

        public ObservableCollection<Video> Videos { get; set; } = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _castNames;

        [ObservableProperty]
        private string _crewNames;

        // Propiedad para MovieDetail
        [ObservableProperty]
        private MovieDetail _movieDetail;

        public string AdultContent => $"Contenido de Adultos: {(MovieDetail?.adult == true ? "Sí" : "No")}";
        public string GenreNames => MovieDetail?.genres != null && MovieDetail.genres.Any()
                                    ? string.Join(", ", MovieDetail.genres.Select(g => g.Name))
    :                               "Sin géneros";

        public async Task InitializeAsync()
        {
            IsBusy = true;
            try
            {
                var trailerTeasers = await _tmdbService.GetTrailersAsync(Media.Id, Media.MediaType);
                if (trailerTeasers?.Any() == true)
                {
                    var trailer = trailerTeasers.FirstOrDefault(t => t.type == "Trailer");
                    if (trailer is null)
                    {
                        trailer = trailerTeasers.First();
                    }
                    MainTrailerUrl = GenerateYoutubeUrl(trailer.key);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Not found", "No se encontraron videos", "Ok");
                }

                // Cargar los detalles de la película
                MovieDetail = await _tmdbService.GetMovieDetailAsync(Media.Id);

                var credits = await _tmdbService.GetMovieCreditsAsync(Media.Id);
                if (credits != null)
                {
                    // Limitar a los primeros 10 actores
                    CastNames = string.Join(", ", credits.Cast
                                               .Take(10)
                                               .Select(c => c.Name));
                    // Filtrar los roles específicos en Crew
                    var relevantJobs = new[] { "Director", "Writer", "Producer", "Executive Producer" };
                    CrewNames = string.Join(", ", credits.Crew
                                                  .Where(c => relevantJobs.Contains(c.Job))
                                                  .Select(c => $"{c.Name} ({c.Job})"));
                }

            }
            finally
            {
                IsBusy = false;
            }
        }
        private static string GenerateYoutubeUrl(string videoKey) =>
            $"https://www.youtube.com/embed/{videoKey}";

    }
}