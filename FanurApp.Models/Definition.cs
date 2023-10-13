using System.ComponentModel.DataAnnotations.Schema;

namespace FanurApp.Models;
public class Definition
{
    public int Id { get; set; }
    public string HMTLText { get; set; }
    public string Author { get; set; }
    public int TopicId { get; set; }
    [ForeignKey("TopicId")]
    public Topic Topic { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}