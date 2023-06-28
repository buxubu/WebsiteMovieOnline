using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string? NameCategory { get; set; }
    [JsonIgnore]
    public virtual ICollection<Movie> Movies { get; } = new List<Movie>();
}
