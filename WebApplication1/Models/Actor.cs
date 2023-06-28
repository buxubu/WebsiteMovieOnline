using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class Actor
{
    public int IdPerson { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Address { get; set; }

    public string? Country { get; set; }

    public string? Sex { get; set; }

    public virtual ICollection<ActorOfAward> ActorOfAwards { get; } = new List<ActorOfAward>();

    public virtual ICollection<MovieCast> MovieCasts { get; } = new List<MovieCast>();
}
