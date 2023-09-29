using System.ComponentModel.DataAnnotations;

namespace FanurApp.ViewModels.Account;

public class RegisterVM
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string ErrorMessage { get; set; }
}