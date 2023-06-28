using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class Reviewer
{
    public int IdRev { get; set; }

    public string? RevName { get; set; }

    public virtual ICollection<Rating> Ratings { get; } = new List<Rating>();
}
