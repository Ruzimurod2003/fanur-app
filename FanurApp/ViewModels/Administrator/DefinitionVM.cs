﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FanurApp.ViewModels.Administrator;
public class DefinitionVM
{
    public int Id { get; set; }
    [Required]
    public string HMTLText { get; set; }
    [Required]
    public int TopicId { get; set; }
    public string TopicName { get; set; }
    public List<SelectListItem> Topics { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    [Required]
    public string Author { get; set; }
    public MessageVM ErrorMessage { get; set; }
}