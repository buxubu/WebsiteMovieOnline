using System;
using System.Collections.Generic;

namespace WebMovieOnline.Models;

public partial class RegisterAccount
{
    public int IdRegister { get; set; }

    public string? Password { get; set; }

    public string? ConfirmPassword { get; set; }

    public string? Email { get; set; }

    public string? Telephone { get; set; }
}
