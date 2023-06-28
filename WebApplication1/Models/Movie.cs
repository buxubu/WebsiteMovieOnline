using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class Movie
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

    public int IdCategory { get; set; }

    public string? TrailerMovie { get; set; }

    public virtual Category IdCategoryNavigation { get; set; } = null!;

    public virtual ICollection<MovieCast> MovieCasts { get; } = new List<MovieCast>();

    public virtual ICollection<MovieOfCinema> MovieOfCinemas { get; } = new List<MovieOfCinema>();

    public virtual ICollection<Rating> Ratings { get; } = new List<Rating>();

    public virtual ICollection<Video> Videos { get; } = new List<Video>();

    public virtual ICollection<ProductionCompany> IdCompanies { get; } = new List<ProductionCompany>();

    public virtual ICollection<Country> IdCountries { get; } = new List<Country>();

    public virtual ICollection<Director> IdDirectors { get; } = new List<Director>();

    public virtual ICollection<Genre> IdGenres { get; } = new List<Genre>();

    public virtual ICollection<Language> IdLanguages { get; } = new List<Language>();
}
