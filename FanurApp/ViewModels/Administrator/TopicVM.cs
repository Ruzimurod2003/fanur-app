using FanurApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace FanurApp.ViewModels.Administrator;
public class TopicVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public List<SelectListItem> Courses { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string Author { get; set; }
    public MessageVM ErrorMessage { get; set; }
}