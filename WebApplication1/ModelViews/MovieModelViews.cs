using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebMovieOnline.ModelViews
{
    public class MovieModelViews
    {
        public int IdMovie { get; set; }

        public string? NameMovie { get; set; }

        public int? Showtimes { get; set; }

        public string? Description { get; set; }

        public bool? Trending { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? Tagline { get; set; }

        public string? MovieStatus { get; set; }

        public string? Images { get; set; }
        public IFormFile? UploadImages { get; set; }
        public string? TrailerMovie { get; set; }
        public IFormFile? UploadVideoTrailer { get; set; }

        public int IdCategory { get; set; }
        public ICollection<int> idLanguages { get; set; }
        public ICollection<int> idGenres { get; set; }
        public ICollection<int> idCountries { get; set; }
        public ICollection<int> idDirectors { get; set; }
        public ICollection<int> idProductionCompanies { get; set; }
    }

    public class MoviesReponse
    {
        public IEnumerable<Movie> ListMovie { get; set; }
        public Paging Pagings { get; set; }
    }

    public class SearchMovie
    {
        public int? page { get; set; } 
        public int pageSize { get; set; } 
        public string? nameMovie { get; set; }
    }
    public class FilteringMovie
    {
        public int? idCategories { get; set; }
        public int? idCountries { get; set; }
        public int? idLanguages { get; set; }
        public int? idGenres { get; set; }
    }
}
