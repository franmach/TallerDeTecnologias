using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ObligatorioTT.Models;
using ObligatorioTT.Pages;
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

        [ObservableProperty]
        private Media _selectedMedia;

        public ObservableCollection<Video> VideosCol { get; set; } = new();
        public ObservableCollection<Media> similarMedia { get; set; } = new();

        [ObservableProperty]
        private int _similarItemWidth = 125;

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _castNames;

        [ObservableProperty]
        private string _crewNames;

        [ObservableProperty]
        private string _videoSource;

        [ObservableProperty]
        private bool _isPlaying;

        // Propiedad para MovieDetail
        [ObservableProperty]
        private MovieDetail _movieDetail;

        public string AdultContent => $"Contenido de Adultos: {(MovieDetail?.adult == true ? "Sí" : "No")}";
        public string GenreNames => MovieDetail?.genres != null && MovieDetail.genres.Any()
                                    ? string.Join(", ", MovieDetail.genres.Select(g => g.Name))
    :                               "Sin géneros";

        public async Task InitializeAsync()
        {
            var similarMediasTask = _tmdbService.GetSimilarAsync(Media.Id, Media.MediaType);
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

                    foreach(var video in trailerTeasers)
                    {
                        VideosCol.Add(video);
                    }
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

            var similarMedias = await similarMediasTask;
            if(similarMedias?.Any() == true)
            {
                foreach(var media in similarMedias)
                {
                    similarMedia.Add(media);
                }
            }
        }
        [RelayCommand]
        
        private async Task ChangeToThisMedia(Media media)
        {
            var parameters = new Dictionary<string, object>
            {
                [nameof(DetailsViewModel.Media)] = media
            };
            await Shell.Current.GoToAsync(nameof(DetailsPage), true, parameters);
        }
        private static string GenerateYoutubeUrl(string videoKey) =>
            $"https://www.youtube.com/embed/{videoKey}";

        [RelayCommand]
        private void PlayVideo(string videoKey)
        {
            VideoSource = $"https://www.youtube.com/embed/{videoKey}";
            IsPlaying = true;
        }
        [RelayCommand]
        private void StopVideo()
        {
            IsPlaying = false;
            VideoSource = string.Empty; // O cualquier URL de una página en blanco
        }

        public ObservableCollection<Media> FavoriteMovies { get; set; } = new();
        [RelayCommand]
        private void AddToFavorites(Media media)
        {
            if (media != null && !FavoriteMovies.Contains(media))
            {
                FavoriteMovies.Add(media);
                // Aquí puedes implementar la persistencia en la base de datos o almacenamiento local si es necesario.
            }
        }

    }
}
