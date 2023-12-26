using System.ComponentModel.DataAnnotations;

namespace FanurApp.ViewModels.Main;

public class ContactUsVM
{
    [Required]
    public string ContactName { get; set; }
    [Required]
    public string ContactPhone { get; set; }
    [Required]
    public string Subject { get; set; }
    [Required]
    public string Message { get; set; }
}