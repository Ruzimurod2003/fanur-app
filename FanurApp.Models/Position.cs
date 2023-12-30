using System.ComponentModel.DataAnnotations.Schema;

namespace FanurApp.Models;
public class Position
{
    public int Id { get; set; }
    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    public int VideoId { get; set; }
    [ForeignKey(nameof(VideoId))]
    public Video Video { get; set; }
    public int ListeningId { get; set; }
    [ForeignKey(nameof(ListeningId))]
    public File Listening { get; set; }
    public int TopicId { get; set; }
    [ForeignKey(nameof(TopicId))]
    public Topic Topic { get; set; }
    public bool IsCompleteTopic { get; set; }
    public bool IsCompleteListening { get; set; }
    public bool IsCompleteVideo { get; set; }
    public DateTime Added { get; set; }
    public DateTime ModifiedOn { get; set; }
}