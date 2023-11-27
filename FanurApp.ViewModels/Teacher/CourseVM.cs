using System.ComponentModel.DataAnnotations;

namespace FanurApp.ViewModels.Teacher;
public class CourseVM
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    [Required]
    public string Author { get; set; }
    public MessageVM ErrorMessage { get; set; }
}