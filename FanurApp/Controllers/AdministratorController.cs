using FanurApp.Enums;
using FanurApp.Models;
using FanurApp.Repositories;
using FanurApp.ViewModels;
using FanurApp.ViewModels.Administrator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FanurApp.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IAdministratorRepository repository;

        public AdministratorController(IAdministratorRepository _repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        #region Courses
        [HttpGet]
        public IActionResult CourseIndex(MessageVM message)
        {
            var courses = repository.GetAllCourses();

            var viewModel = new CourseIndexVM()
            {
                Courses = courses.Select(i => new CourseVM()
                {
                    Id = i.Id,
                    Author = i.Author,
                    Name = i.Name,
                    CreatedDate = i.CreatedDate,
                    Description = i.Description,
                    UpdatedDate = i.UpdatedDate
                }).ToList()
            };

            if (message != null)
            {
                viewModel.Message = message;
            }

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Course(int? id)
        {
            var viewModel = new CourseVM();
            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            if (id != 0 && id.HasValue)
            {
                var course = repository.GetCourseById(id.Value);
                viewModel.Id = id.Value;
                viewModel.Name = course.Name;
                viewModel.Description = course.Description;
                viewModel.Author = course.Author;
                viewModel.CreatedDate = course.CreatedDate;
                viewModel.UpdatedDate = course.UpdatedDate;
            }

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Course(CourseVM viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Id != 0)
                {
                    var course = new Course();
                    course.Author = viewModel.Author;
                    course.Name = viewModel.Name;
                    course.Description = viewModel.Description;
                    var resultUpdate = repository.UpdateCourse(viewModel.Id, course);
                    if (resultUpdate)
                    {
                        return RedirectToAction("CourseIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = "Kurslar muvofaqiyatli o'zgartirildi"
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = "Kursni o'zgartirib bo'lmaydi"
                        };
                    }
                }
                else
                {
                    var course = new Course();
                    course.Author = viewModel.Author;
                    course.Name = viewModel.Name;
                    course.Description = viewModel.Description;
                    course.CreatedDate = DateTime.Now;
                    course.UpdatedDate = DateTime.Now;
                    var resultAdd = repository.AddCourse(course);
                    if (resultAdd)
                    {
                        return RedirectToAction("CourseIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = "Kurslar muvofaqiyatli yaratildi"
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = "Kursni yaratib bo'lmaydi"
                        };
                    }
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteCourse(int id)
        {
            var result = repository.DeleteCourse(id);
            if (result)
            {
                return RedirectToAction("CourseIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Success,
                    MessageText = "Kurslar muvofaqiyatli o'chirildi"
                });
            }
            else
            {

                return RedirectToAction("CourseIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = "Kurslar o'chirilishi bilan problema bor"
                });
            }
        }
        #endregion

        #region Topics
        [HttpGet]
        public IActionResult TopicIndex(MessageVM message)
        {
            var topics = repository.GetAllTopics();
            var courses = repository.GetAllCourses();

            var viewModel = new TopicIndexVM()
            {
                Topics = topics.Select(i => new TopicVM()
                {
                    Id = i.Id,
                    Author = i.Author,
                    CourseId = i.CourseId,
                    CourseName = i.Course.Name,
                    Name = i.Name,
                    CreatedDate = i.CreatedDate,
                    Description = i.Description,
                    UpdatedDate = i.UpdatedDate,
                    Courses = courses.Select(i => new SelectListItem()
                    {
                        Value = i.Id.ToString(),
                        Text = i.Name
                    }).ToList()
                }).ToList()
            };

            if (message != null)
            {
                viewModel.Message = message;
            }

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Topic(int? id)
        {
            var viewModel = new TopicVM();
            var courses = repository.GetAllCourses();

            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            viewModel.Courses = courses
                .Select(i => new SelectListItem()
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                }).ToList();

            if (id != 0 && id.HasValue)
            {
                var topic = repository.GetTopicById(id.Value);
                viewModel.Id = id.Value;
                viewModel.Name = topic.Name;
                viewModel.CourseId = topic.CourseId;
                viewModel.Description = topic.Description;
                viewModel.Author = topic.Author;
                viewModel.CreatedDate = topic.CreatedDate;
                viewModel.UpdatedDate = topic.UpdatedDate;
            }

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Topic(TopicVM viewModel)
        {
            var courses = repository.GetAllCourses();
            viewModel.Courses = courses
                .Select(i => new SelectListItem()
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                }).ToList();

            if (ModelState.IsValid)
            {
                if (viewModel.Id != 0)
                {
                    var topic = new Topic();
                    topic.Author = viewModel.Author;
                    topic.Name = viewModel.Name;
                    topic.CourseId = viewModel.CourseId;
                    topic.Description = viewModel.Description;
                    var resultUpdate = repository.UpdateTopic(viewModel.Id, topic);
                    if (resultUpdate)
                    {
                        return RedirectToAction("TopcIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = "Mavzular muvofaqiyatli o'zgartirildi"
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = "Mavzuni o'zgartirib bo'lmaydi"
                        };
                    }
                }
                else
                {
                    var topic = new Topic();
                    topic.Author = viewModel.Author;
                    topic.Name = viewModel.Name;
                    topic.CourseId = viewModel.CourseId;
                    topic.Description = viewModel.Description;
                    topic.CreatedDate = DateTime.Now;
                    topic.UpdatedDate = DateTime.Now;
                    var resultAdd = repository.AddTopic(topic);
                    if (resultAdd)
                    {
                        return RedirectToAction("TopicIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = "Mavzular muvofaqiyatli yaratildi"
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = "Mavzuni yaratib bo'lmaydi"
                        };
                    }
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteTopic(int id)
        {
            var result = repository.DeleteTopic(id);
            if (result)
            {
                return RedirectToAction("TopicIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Success,
                    MessageText = "Mavzular muvofaqiyatli o'chirildi"
                });
            }
            else
            {

                return RedirectToAction("TopicIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = "Mavzularni o'chirilishi bilan problema bor"
                });
            }
        }
        #endregion

        #region Videos
        [HttpGet]
        public IActionResult VideoIndex(MessageVM message)
        {
            var videos = repository.GetAllVideos();
            var topics = repository.GetAllTopics();

            var viewModel = new VideoIndexVM()
            {
                Videos = videos.Select(i => new VideoVM()
                {
                    Id = i.Id,
                    Author = i.Author,
                    TopicId = i.TopicId,
                    URLName = i.URLName,
                    TopicName = i.Topic.Name,
                    CreatedDate = i.CreatedDate,
                    Caption = i.Caption,
                    UpdatedDate = i.UpdatedDate,
                    Topics = topics.Select(i => new SelectListItem()
                    {
                        Value = i.Id.ToString(),
                        Text = i.Name
                    }).ToList()
                }).ToList()
            };

            if (message != null)
            {
                viewModel.Message = message;
            }

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Video(int? id)
        {
            var viewModel = new VideoVM();
            var topics = repository.GetAllTopics();

            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            viewModel.Topics = topics
                .Select(i => new SelectListItem()
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                }).ToList();

            if (id != 0 && id.HasValue)
            {
                var video = repository.GetVideoById(id.Value);
                viewModel.Id = id.Value;
                viewModel.URLName = video.URLName;
                viewModel.TopicId = video.TopicId;
                viewModel.Caption = video.Caption;
                viewModel.Author = video.Author;
                viewModel.CreatedDate = video.CreatedDate;
                viewModel.UpdatedDate = video.UpdatedDate;
            }

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Video(VideoVM viewModel)
        {
            var topics = repository.GetAllTopics();
            viewModel.Topics = topics
                .Select(i => new SelectListItem()
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                }).ToList();

            if (ModelState.IsValid)
            {
                if (viewModel.Id != 0)
                {
                    var video = new Video();
                    video.Author = viewModel.Author;
                    video.URLName = viewModel.URLName;
                    video.TopicId = viewModel.TopicId;
                    video.Caption = viewModel.Caption;
                    var resultUpdate = repository.UpdateVideo(viewModel.Id, video);
                    if (resultUpdate)
                    {
                        return RedirectToAction("VideoIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = "Videolar muvofaqiyatli o'zgartirildi"
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = "Videoni o'zgartirib bo'lmaydi"
                        };
                    }
                }
                else
                {
                    var video = new Video();
                    video.Author = viewModel.Author;
                    video.URLName = viewModel.URLName;
                    video.TopicId = viewModel.TopicId;
                    video.Caption = viewModel.Caption;
                    video.CreatedDate = DateTime.Now;
                    video.UpdatedDate = DateTime.Now;
                    var resultAdd = repository.AddVideo(video);
                    if (resultAdd)
                    {
                        return RedirectToAction("VideoIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = "Videolar muvofaqiyatli yaratildi"
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = "Videoni yaratib bo'lmaydi"
                        };
                    }
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteVideo(int id)
        {
            var result = repository.DeleteVideo(id);
            if (result)
            {
                return RedirectToAction("VideoIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Success,
                    MessageText = "Video muvofaqiyatli o'chirildi"
                });
            }
            else
            {

                return RedirectToAction("TopicIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = "Videolarni o'chirilishi bilan problema bor"
                });
            }
        }
        #endregion

        #region Definitions
        [HttpGet]
        public IActionResult DefinitionIndex(MessageVM message)
        {
            var definitions = repository.GetAllDefinitions();
            var topics = repository.GetAllTopics();

            var viewModel = new DefinitionIndexVM()
            {
                Definitions = definitions.Select(i => new DefinitionVM()
                {
                    Id = i.Id,
                    Author = i.Author,
                    TopicId = i.TopicId,
                    HMTLText = i.HMTLText,
                    TopicName = i.Topic.Name,
                    CreatedDate = i.CreatedDate,
                    UpdatedDate = i.UpdatedDate,
                    Topics = topics.Select(i => new SelectListItem()
                    {
                        Value = i.Id.ToString(),
                        Text = i.Name
                    }).ToList()
                }).ToList()
            };

            if (message != null)
            {
                viewModel.Message = message;
            }

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Definition(int? id)
        {
            var viewModel = new DefinitionVM();
            var topics = repository.GetAllTopics();

            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            viewModel.Topics = topics
                .Select(i => new SelectListItem()
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                }).ToList();

            if (id != 0 && id.HasValue)
            {
                var video = repository.GetDefinitionById(id.Value);
                viewModel.Id = id.Value;
                viewModel.HMTLText = video.HMTLText;
                viewModel.TopicId = video.TopicId;
                viewModel.Author = video.Author;
                viewModel.CreatedDate = video.CreatedDate;
                viewModel.UpdatedDate = video.UpdatedDate;
            }

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Definition(DefinitionVM viewModel)
        {
            var topics = repository.GetAllTopics();
            viewModel.Topics = topics
                .Select(i => new SelectListItem()
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                }).ToList();

            if (ModelState.IsValid)
            {
                if (viewModel.Id != 0)
                {
                    var definition = new Definition();
                    definition.Author = viewModel.Author;
                    definition.HMTLText = viewModel.HMTLText;
                    definition.TopicId = viewModel.TopicId;
                    var resultUpdate = repository.UpdateDefinition(viewModel.Id, definition);
                    if (resultUpdate)
                    {
                        return RedirectToAction("DefinitionIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = "Definitionlar muvofaqiyatli o'zgartirildi"
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = "Definitionni o'zgartirib bo'lmaydi"
                        };
                    }
                }
                else
                {
                    var definition = new Definition();
                    definition.Author = viewModel.Author;
                    definition.HMTLText = viewModel.HMTLText;
                    definition.TopicId = viewModel.TopicId;
                    definition.CreatedDate = DateTime.Now;
                    definition.UpdatedDate = DateTime.Now;
                    var resultAdd = repository.AddDefinition(definition);
                    if (resultAdd)
                    {
                        return RedirectToAction("DefinitionIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = "Definitionlar muvofaqiyatli yaratildi"
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = "Definitionni yaratib bo'lmaydi"
                        };
                    }
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteDefinition(int id)
        {
            var result = repository.DeleteDefinition(id);
            if (result)
            {
                return RedirectToAction("DefinitionIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Success,
                    MessageText = "Definition muvofaqiyatli o'chirildi"
                });
            }
            else
            {

                return RedirectToAction("DefinitionIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = "Definitionlarni o'chirilishi bilan problema bor"
                });
            }
        }
        #endregion

        #region Resources
        [HttpGet]
        public IActionResult ResourceIndex(MessageVM message)
        {
            var resources = repository.GetAllResources();
            var cultures = repository.GetAllCultures();

            var viewModel = new ResourceIndexVM()
            {
                Resources = resources.Select(i => new ResourceVM()
                {
                    Id = i.Id,
                    Key = i.Key,
                    Value = i.Value,
                    CultureId = i.CultureId,
                    CultureName = i.Culture.Name,
                    CreatedDate = i.CreatedDate,
                    UpdatedDate = i.UpdatedDate,
                    Cultures = cultures.Select(i => new SelectListItem()
                    {
                        Value = i.Id.ToString(),
                        Text = i.Name
                    }).ToList()
                }).ToList()
            };

            if (message != null)
            {
                viewModel.Message = message;
            }

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Resource(int? id)
        {
            var viewModel = new ResourceVM();
            var cultures = repository.GetAllCultures();

            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            viewModel.Cultures = cultures
                .Select(i => new SelectListItem()
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                }).ToList();

            if (id != 0 && id.HasValue)
            {
                var resource = repository.GetResourceById(id.Value);
                viewModel.Id = id.Value;
                viewModel.Key = resource.Key;
                viewModel.Value = resource.Value;
                viewModel.CultureId = resource.CultureId;
                viewModel.CreatedDate = resource.CreatedDate;
                viewModel.UpdatedDate = resource.UpdatedDate;
            }

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Resource(ResourceVM viewModel)
        {

            var cultures = repository.GetAllCultures();
            viewModel.Cultures = cultures
                .Select(i => new SelectListItem()
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                }).ToList();

            if (ModelState.IsValid)
            {
                if (viewModel.Id != 0)
                {
                    var resource = new Resource();
                    resource.Key = viewModel.Key;
                    resource.Value = viewModel.Value;
                    resource.CultureId = viewModel.CultureId;
                    var resultUpdate = repository.UpdateResource(viewModel.Id, resource);
                    if (resultUpdate)
                    {
                        return RedirectToAction("ResourceIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = "Tillar muvofaqiyatli o'zgartirildi"
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = "Tilni o'zgartirib bo'lmaydi"
                        };
                    }
                }
                else
                {
                    var resource = new Resource();
                    resource.Key = viewModel.Key;
                    resource.Value = viewModel.Value;
                    resource.CultureId = viewModel.CultureId;
                    resource.CreatedDate = DateTime.Now;
                    resource.UpdatedDate = DateTime.Now;
                    var resultAdd = repository.AddResource(resource);
                    if (resultAdd)
                    {
                        return RedirectToAction("ResourceIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = "Tillar muvofaqiyatli yaratildi"
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = "Tilni yaratib bo'lmaydi"
                        };
                    }
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteResource(int id)
        {
            var result = repository.DeleteResource(id);
            if (result)
            {
                return RedirectToAction("ResourceIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Success,
                    MessageText = "Tillar muvofaqiyatli o'chirildi"
                });
            }
            else
            {

                return RedirectToAction("ResourceIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = "Tillar o'chirilishi bilan problema bor"
                });
            }
        }
        #endregion
    }
}
