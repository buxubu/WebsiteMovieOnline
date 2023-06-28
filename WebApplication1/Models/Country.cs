using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class Country
{
    public int IdCountry { get; set; }

    public string? NameCountry { get; set; }
    [JsonIgnore]

    public virtual ICollection<Movie> IdMovies { get; } = new List<Movie>();
}
