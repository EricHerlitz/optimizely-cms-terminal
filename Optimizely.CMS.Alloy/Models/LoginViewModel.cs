using System.ComponentModel.DataAnnotations;

namespace Optimizely.CMS.Alloy.Models;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
