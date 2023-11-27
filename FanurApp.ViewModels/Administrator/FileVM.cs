using System.ComponentModel.DataAnnotations;

namespace FanurApp.ViewModels.Administrator;
public class FileVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public int FileTypeId { get; set; }
    public DateTime CreatedDate { get; set; }
    public int TopicId { get; set; }
    public string TopicName { get; set; }
    public MessageVM ErrorMessage { get; set; }
}