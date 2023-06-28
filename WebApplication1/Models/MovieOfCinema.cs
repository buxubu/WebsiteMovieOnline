using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class MovieOfCinema
{
    public int IdMovie { get; set; }

    public int IdCinema { get; set; }

    public DateTime? DateFirstShowing { get; set; }

    public DateTime? DateLastShowing { get; set; }

    public virtual Cinema IdCinemaNavigation { get; set; } = null!;

    public virtual Movie IdMovieNavigation { get; set; } = null!;
}
