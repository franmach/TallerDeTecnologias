using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using ObligatorioTT.Models;
using ObligatorioTT.Services;

namespace ObligatorioTT.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly TmdbService _tmdbService;
        public HomeViewModel(TmdbService tmdbService) 
        {
            _tmdbService = tmdbService;
        }
 
        [ObservableProperty]
        private Media _trendingMovie;

        public ObservableCollection<Media> Trending { get; set; } = new();
        public ObservableCollection<Media> TopRated { get; set; } = new();
        public ObservableCollection<Media> Popular { get; set; } = new();
        public ObservableCollection<Media> Upcoming { get; set; } = new();

        public async Task InitializeAsync()
        {
            var trendingListTask =  _tmdbService.GetTrendingAsync();
            var TopRatedListTask =  _tmdbService.GetTopRatedAsync();
            var popularListTask =  _tmdbService.GetPopularAsync();
            var upcomingListTask =  _tmdbService.GetUpcomingAsync();

            var medias = await Task.WhenAll(trendingListTask,
                                            TopRatedListTask,
                                            popularListTask,
                                            upcomingListTask);

            var trendingList = medias[0];
            var TopRatedList = medias[1];
            var popularList = medias[2];
            var upcomingList = medias[3];

            //setea peliculas random de la lista trendingList
            TrendingMovie = trendingList.OrderBy(t => Guid.NewGuid())
                                     .FirstOrDefault(t => !string.IsNullOrWhiteSpace(t.DisplayTitle)
                                     && !string.IsNullOrWhiteSpace(t.Thumbnail));

            SetMediaCollection(trendingList, Trending);
            SetMediaCollection(TopRatedList, TopRated);
            SetMediaCollection(popularList, Popular);
            SetMediaCollection(upcomingList, Upcoming);
        }
        private static void SetMediaCollection(IEnumerable<Media> medias, ObservableCollection<Media> collection)
        {
            collection.Clear();
            foreach(var media in medias)
            {
                collection.Add(media);
            }
        }
    }
}
