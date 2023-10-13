using System.ComponentModel.DataAnnotations.Schema;

namespace FanurApp.Models;

public class Resource
{
    public int Id { get; set; }
    public string Key { get; set; }     // ключ
    public string Value { get; set; }   // значение
    public int CultureId { get; set; }
    [ForeignKey("CultureId")]
    public Culture Culture { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}