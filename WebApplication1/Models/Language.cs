using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class Language
{
    public int IdLanguage { get; set; }

    public string? NameLanguage { get; set; }

    public virtual ICollection<Movie> IdMovies { get; } = new List<Movie>();
}
