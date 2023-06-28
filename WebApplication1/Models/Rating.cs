using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class Rating
{
    public int IdMovie { get; set; }

    public int IdRev { get; set; }

    public int? RevStar { get; set; }

    public int? RatingCount { get; set; }

    public virtual Movie IdMovieNavigation { get; set; } = null!;

    public virtual Reviewer IdRevNavigation { get; set; } = null!;
}
