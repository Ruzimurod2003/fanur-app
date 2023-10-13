using System.ComponentModel.DataAnnotations.Schema;

namespace FanurApp.Models;

public class Video
{
    public int Id { get; set; }
    public string URLName { get; set; }
    public string Caption { get; set; }
    public int TopicId { get; set; }
    [ForeignKey("TopicId")]
    public Topic Topic { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string Author { get; set; }
}