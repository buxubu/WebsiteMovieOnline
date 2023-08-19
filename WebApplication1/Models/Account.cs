using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class Account
{
    public int IdAccount { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public bool? IsAdmin { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Role { get; set; }
}
