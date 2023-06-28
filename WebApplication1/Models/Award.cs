using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class Award
{
    public int IdAward { get; set; }

    public string? NameAward { get; set; }

    public virtual ICollection<ActorOfAward> ActorOfAwards { get; } = new List<ActorOfAward>();
}
