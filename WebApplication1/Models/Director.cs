using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class Director
{
    public int IdDirector { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? Birhday { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Movie> IdMovies { get; } = new List<Movie>();
}
