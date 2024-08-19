using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ObligatorioTT.Models;
using ObligatorioTT.Pages;
using ObligatorioTT.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ObligatorioTT.ViewModels
{
    public partial class SearchViewModel : ObservableObject
    {
        private readonly TmdbService _tmdbService;

        public SearchViewModel(TmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        [ObservableProperty]
        private string _searchQuery;

        public ObservableCollection<Media> SearchResults { get; set; } = new();

        [RelayCommand]
        private async Task Search(string query)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                var results = await _tmdbService.SearchMoviesAsync(query);
                SearchResults.Clear();
                foreach (var movie in results)
                {
                    SearchResults.Add(movie);
                }
            }
        }

        [RelayCommand]
        private async Task NavigateToDetail(Media selectedMedia)
        {
            var parameters = new Dictionary<string, object>
            {
                { nameof(DetailsViewModel.Media), selectedMedia }
            };

            await Shell.Current.GoToAsync(nameof(DetailsPage), true, parameters);
        }
    }
}
