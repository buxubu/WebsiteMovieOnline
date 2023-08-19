using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class ProductionCompany
{
    public int IdCompany { get; set; }

    public string? NameCompany { get; set; }

    public virtual ICollection<Movie> IdMovies { get; } = new List<Movie>();
}
