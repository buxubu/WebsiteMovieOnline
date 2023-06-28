using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class Video
{
    public int IdVideo { get; set; }

    public string? NameVideo { get; set; }

    public int? Episode { get; set; }

    public int IdMovie { get; set; }
    [JsonIgnore]

    public virtual Movie IdMovieNavigation { get; set; } = null!;
}
