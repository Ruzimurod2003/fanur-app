using System.ComponentModel.DataAnnotations.Schema;

namespace FanurApp.Models;
public class File
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public int TopicId { get; set; }
    [ForeignKey(nameof(TopicId))]
    public Topic Topic { get; set; }
    public int FileTypeId { get; set; }
    public DateTime CreatedDate { get; set; }
}