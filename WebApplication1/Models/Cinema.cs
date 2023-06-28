using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class Cinema
{
    public int IdCinema { get; set; }

    public string? NameCinema { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public virtual ICollection<MovieOfCinema> MovieOfCinemas { get; } = new List<MovieOfCinema>();
}
