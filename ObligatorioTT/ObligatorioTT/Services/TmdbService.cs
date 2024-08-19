using ObligatorioTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace ObligatorioTT.Services
{
    public class TmdbService
    {
        private const string ApiKey = "7aa67c19f9c8135507e8aaf245874e41";
        public const string TmdbHttpClientName = "TmdbClient";
        private readonly IHttpClientFactory _httpClientFactory;

        public TmdbService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient HttpClient => _httpClientFactory.CreateClient(TmdbHttpClientName);

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            var genresWrapper = await HttpClient.GetFromJsonAsync<GenreWrapper>($"{TmdbUrls.MoviesGenres}&api_key={ApiKey}");
            return genresWrapper.Genres;
        }
        public async Task<IEnumerable<Media>> GetTrendingAsync() =>
            await GetMediasAsync(TmdbUrls.Trending);

        public async Task<IEnumerable<Media>> GetTopRatedAsync() =>
            await GetMediasAsync(TmdbUrls.TopRated);
        public async Task<IEnumerable<Media>> GetPopularAsync() =>
            await GetMediasAsync(TmdbUrls.Popular);
        public async Task<IEnumerable<Media>> GetUpcomingAsync() =>
            await GetMediasAsync(TmdbUrls.Upcoming);

        public async Task<IEnumerable<Media>> GetActionAsync() =>
            await GetMediasAsync(TmdbUrls.Action);

        public async Task<IEnumerable<Media>> GetSimilarAsync(int id, string type = "movie") =>
            await GetMediasAsync(
                $"{TmdbUrls.GetSimilar(id, type)}&api_key={ApiKey}");

        public async Task<IEnumerable<Video>?> GetTrailersAsync(int id, string type = "movie")
        {
            var videosWrapper = await HttpClient.GetFromJsonAsync<VideosWrapper>
                ($"{TmdbUrls.GetTrailers(id, type)}&api_key={ApiKey}");

            if (videosWrapper?.results.Length > 0)
            {
                var trailerTeasers = videosWrapper.results.Where(VideosWrapper.FilterTrailerTeasers);
                return trailerTeasers;
            }
            return null;
        }
        private async Task<IEnumerable<Media>> GetMediasAsync(string url)
        {
            var trendingMoviesCollections = await HttpClient.GetFromJsonAsync<Movie>($"{url}&api_key={ApiKey}");
            return trendingMoviesCollections.results.Select(r => r.ToMediaObject());
        }

        public async Task<MovieDetail> GetMovieDetailAsync(int movieId)
        {
            return await HttpClient.GetFromJsonAsync<MovieDetail>($"{TmdbUrls.GetMovieDetails(movieId)}&api_key={ApiKey}");
        }
        public async Task<Credits> GetMovieCreditsAsync(int movieId)
        {
            string url = $"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key={ApiKey}";

            var response = await HttpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Credits>();
            }
            else
            {
                // Manejo del error si la solicitud no fue exitosa
                return null;
            }
        }

        public async Task<IEnumerable<Media>> SearchMoviesAsync(string query)
        {
            var url = $"https://api.themoviedb.org/3/search/movie?query={query}&api_key={ApiKey}";
            var searchResult = await HttpClient.GetFromJsonAsync<Movie>(url);
            return searchResult.results.Select(r => r.ToMediaObject());
        }

    }

    public static class TmdbUrls
    {
        public const string Trending = "3/trending/all/week?language=es-sp";
        public const string TopRated = "3/movie/top_rated?language=es-sp";
        public const string Popular = "3/movie/popular?language=es-sp";
        public const string Upcoming = "3/movie/upcoming?language=es-sp";
        public const string Action = "3/discover/movie?language=es-sp&with_genres=28";
        public const string MoviesGenres = "3/genre/movie/list?language=es-sp";
        public static string GetTrailers(int movieId, string type = "movie") => $"3/{type ?? "movie"}/{movieId}/videos?language=es-sp";
        public static string GetMovieDetails(int movieId, string type = "movie") => $"3/{type ?? "movie"}/{movieId}?language=es-sp";
        public static string GetSimilar(int movieId, string type = "movie") => $"3/{type ?? "movie"}/{movieId}/similar?language=es-sp";

    }


    public class Movie
    {
        public int page { get; set; }
        public Result[] results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }

    }
    public class Result
    {
        public string backdrop_path { get; set; }
        public int[] genre_ids { get; set; }
        public int id { get; set; }
        public string original_title { get; set; }
        public string original_name { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public bool video { get; set; }
        public string media_type { get; set; } // "movie" or "tv"
        public string ThumbnailPath => poster_path ?? backdrop_path;
        public string Thumbnail => $"https://image.tmdb.org/t/p/w600_and_h900_bestv2/{ThumbnailPath}";
        public string ThumbnailSmall => $"https://image.tmdb.org/t/p/w220_and_h330_face/{ThumbnailPath}";
        public string ThumbnailUrl => $"https://image.tmdb.org/t/p/original/{ThumbnailPath}";
        public string DisplayTitle => title ?? name ?? original_title ?? original_name;

        public Media ToMediaObject() =>
            new()
            {
                Id = id,
                DisplayTitle = DisplayTitle,
                MediaType = media_type,
                Overview = overview,
                ReleaseDate = release_date,
                Thumbnail = Thumbnail,
                ThumbnailSmall = ThumbnailSmall,
                ThumbnailUrl = ThumbnailUrl
            };
    }
    public class VideosWrapper
    {
        public int id { get; set; }
        public Video[] results { get; set; }

        public static Func<Video, bool> FilterTrailerTeasers => v =>
            v.official
            && v.site.Equals("Youtube", StringComparison.OrdinalIgnoreCase)
            && (v.type.Equals("Teaser", StringComparison.OrdinalIgnoreCase) || v.type.Equals("Trailer", StringComparison.OrdinalIgnoreCase));
    }
    public class Video
    {
        public string name { get; set; }
        public string key { get; set; }
        public string site { get; set; }
        public string type { get; set; }
        public bool official { get; set; }
        public DateTime published_at { get; set; }
        public string Thumbnail => $"https://i.ytimg.com/vi/{key}/mqdefault.jpg";
    }
    public class MovieDetail
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public object belongs_to_collection { get; set; }
        public int budget { get; set; }
        public Genre[] genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public Production_Companies[] production_companies { get; set; }
        public Production_Countries[] production_countries { get; set; }
        public string release_date { get; set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        public Spoken_Languages[] spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
    }
    public class Production_Companies
    {
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }
    public class Production_Countries
    {
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
    }
    public class Spoken_Languages
    {
        public string english_name { get; set; }
        public string iso_639_1 { get; set; }
        public string name { get; set; }
    }
    public class GenreWrapper
    {
        public IEnumerable<Genre> Genres { get; set; }
    }
    public record struct Genre(int Id, string Name);

    public class Credits
    {
        public int Id { get; set; }
        public List<CastMember> Cast { get; set; }
        public List<CrewMember> Crew { get; set; }
    }

    public class CastMember
    {
        public string Name { get; set; }
        public string Character { get; set; }
        public string ProfilePath { get; set; }
    }

    public class CrewMember
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }
    }
}


