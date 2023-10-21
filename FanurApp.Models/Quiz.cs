using System.ComponentModel.DataAnnotations.Schema;

namespace FanurApp.Models;
public class Quiz
{
    public int Id { get; set; }
    public string QuestionText { get; set; }
    public int TopicId { get; set; }
    [ForeignKey(nameof(TopicId))]
    public Topic Topic { get; set; }
    public string AnswerA { get; set; }
    public string AnswerB { get; set; }
    public string AnswerC { get; set; }
    public string AnswerD { get; set; }
    public string IsTrueAnswer { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}