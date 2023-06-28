using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class ActorOfAward
{
    public int IdPerson { get; set; }

    public int IdAward { get; set; }

    public DateTime? AwardYear { get; set; }

    public virtual Award IdAwardNavigation { get; set; } = null!;

    public virtual Actor IdPersonNavigation { get; set; } = null!;
}
