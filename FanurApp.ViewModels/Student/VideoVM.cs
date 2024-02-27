namespace FanurApp.ViewModels.Student;
public class VideoVM
{
    public int TopicId { get; set; }
    public string VideoURL { get; set; }
    public string TopicName { get; set; }
    public int? PreviousTopicId { get; set; }
    public int? NextTopicId { get; set; }
}