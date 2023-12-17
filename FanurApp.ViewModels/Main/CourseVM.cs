using System.ComponentModel.DataAnnotations;

namespace FanurApp.ViewModels.Main;
public class CourseVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string Author { get; set; }
}