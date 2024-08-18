using CommunityToolkit.Mvvm.ComponentModel;
using ObligatorioTT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using ObligatorioTT.Services;
using System.Collections.ObjectModel;

namespace ObligatorioTT.ViewModels
{
    public partial class CategoriesViewModel : ObservableObject
    {
        private readonly TmdbService _tmdbService;

        private IEnumerable<Genre> _genreList;
        public CategoriesViewModel(TmdbService tmdbService)
        {
            Categories = new ObservableCollection<string>(
                new string[] { "Mi Lista", "Descargas" });
            _tmdbService = tmdbService;
        }
        public ObservableCollection<string> Categories { get; set; } = new();

        public async Task InitializeAsync()
        {
            
            _genreList ??= await _tmdbService.GetGenresAsync();
            
            Categories.Clear();
            Categories.Add("Mi Lista");
            Categories.Add("Descargas");


            foreach (var genre in _genreList)
            {
                Categories.Add(genre.Name);
            }
        }
    }
}
