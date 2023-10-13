using System.ComponentModel.DataAnnotations;

namespace FanurApp.ViewModels.Administrator;

public class ResourceVM
{
    public int Id { get; set; }
    [Required]
    public string Key { get; set; }
    [Required]
    public string Value { get; set; }
    [Required]
    public int CultureId { get; set; }
    public string CultureName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public List<CultureVM> Cultures { get; set; }
    public MessageVM ErrorMessage { get; set; }
}