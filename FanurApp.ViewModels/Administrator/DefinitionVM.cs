using System.ComponentModel.DataAnnotations;

namespace FanurApp.ViewModels.Administrator;
public class DefinitionVM
{
    public int Id { get; set; }
    [Required]
    public string HMTLText { get; set; }
    [Required]
    public int TopicId { get; set; }
    public string TopicName { get; set; }
    public List<TopicVM> Topics { get; set; }
    [Required]
    public int CourseId { get; set; }
    public List<CourseVM> Courses { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    [Required]
    public string Author { get; set; }
    public MessageVM ErrorMessage { get; set; }
}