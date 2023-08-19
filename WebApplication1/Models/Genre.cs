using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class Genre
{
    public int IdGenre { get; set; }

    public string? NameGenre { get; set; }

    public virtual ICollection<Movie> IdMovies { get; } = new List<Movie>();
}
