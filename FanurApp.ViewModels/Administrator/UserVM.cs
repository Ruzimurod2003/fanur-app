using System.ComponentModel.DataAnnotations;

namespace FanurApp.ViewModels.Administrator;
public class UserVM
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public List<RoleVM> Roles { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public MessageVM ErrorMessage { get; set; }
}