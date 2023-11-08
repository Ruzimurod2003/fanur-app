using System.ComponentModel.DataAnnotations;

namespace FanurApp.ViewModels.Administrator;
public class VideoVM
{
    public int Id { get; set; }
    [Required]
    public string URLName { get; set; }
    [Required]
    public string Caption { get; set; }
    [Required]
    public int CourseId { get; set; }
    public List<CourseVM> Courses { get; set; }
    [Required]
    public int TopicId { get; set; }
    public string TopicName { get; set; }
    public List<TopicVM> Topics { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    [Required]
    public string Author { get; set; }
    public MessageVM ErrorMessage { get; set; }
}