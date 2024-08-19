using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ObligatorioTT.Models;
using System.Collections.ObjectModel;

namespace ObligatorioTT.ViewModels
{
    public partial class FavoritesViewModel : ObservableObject
    {
        // Colección que almacena las películas favoritas
        public ObservableCollection<Media> FavoriteMovies { get; set; } = new();

        // Comando para agregar una película a la lista de favoritos
        [RelayCommand]
        public void AddToFavorites(Media media)
        {
            if (media != null && !FavoriteMovies.Contains(media))
            {
                FavoriteMovies.Add(media);
                // Aquí puedes implementar la persistencia en la base de datos o almacenamiento local si es necesario.
            }
        }

        // Comando para eliminar una película de la lista de favoritos
        [RelayCommand]
        public void RemoveFromFavorites(Media media)
        {
            if (media != null && FavoriteMovies.Contains(media))
            {
                FavoriteMovies.Remove(media);
                // Aquí puedes implementar la lógica para eliminar de la base de datos o almacenamiento local si es necesario.
            }
        }

        // Método para cargar la lista de favoritos desde el almacenamiento
        public async Task LoadFavoritesAsync()
        {
            // Aquí puedes implementar la lógica para cargar las películas favoritas desde la base de datos o almacenamiento local.
            // Por ejemplo:
            // var favoriteMoviesFromDb = await _databaseService.GetFavoriteMoviesAsync();
            // foreach (var movie in favoriteMoviesFromDb)
            // {
            //     FavoriteMovies.Add(movie);
            // }
        }
    }
}