using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class MovieCast
{
    public int IdMovie { get; set; }

    public int IdPerson { get; set; }

    public string? CharacterName { get; set; }

    public string? Position { get; set; }
    [JsonIgnore]
    public virtual Movie IdMovieNavigation { get; set; } = null!;

    public virtual Actor IdPersonNavigation { get; set; } = null!;
}
