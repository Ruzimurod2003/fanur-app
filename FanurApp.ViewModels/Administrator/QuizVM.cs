using System.ComponentModel.DataAnnotations;

namespace FanurApp.ViewModels.Administrator;
public class QuizVM
{
    public int Id { get; set; }
    [Required]
    public string QuestionText { get; set; }
    [Required]
    public int TopicId { get; set; }
    public TopicVM Topic { get; set; }
    public string TopicName { get; set; }
    [Required]
    public int CourseId { get; set; }
    public List<CourseVM> Courses { get; set; }
    [Required]
    public string AnswerA { get; set; }
    [Required]
    public string AnswerB { get; set; }
    [Required]
    public string AnswerC { get; set; }
    [Required]
    public string AnswerD { get; set; }
    [Required]
    public string IsTrueAnswer { get; set; }
    public List<TopicVM> Topics { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public MessageVM ErrorMessage { get; set; }
}