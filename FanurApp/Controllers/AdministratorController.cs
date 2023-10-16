using AutoMapper;
using FanurApp.Commons.Enums;
using FanurApp.Models;
using FanurApp.Repositories;
using FanurApp.ViewModels;
using FanurApp.ViewModels.Administrator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FanurApp.Controllers
{
    [Authorize(Roles = "Administrator")]
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

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Course, CourseVM>());
            var mapper = new Mapper(config);

            var viewModel = new CourseIndexVM()
            {
                Courses = courses.Select(i => mapper.Map<CourseVM>(i)).ToList()
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

            if (id != 0 && id.HasValue)
            {
                var course = repository.GetCourseById(id.Value);

                var config = new MapperConfiguration(cfg => cfg.CreateMap<Course, CourseVM>());
                var mapper = new Mapper(config);

                viewModel = mapper.Map<CourseVM>(course);
            }

            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Course(CourseVM viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Id != 0)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<CourseVM, Course>());
                    var mapper = new Mapper(config);
                    var course = mapper.Map<Course>(viewModel);

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
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<CourseVM, Course>());
                    var mapper = new Mapper(config);
                    var course = mapper.Map<Course>(viewModel);

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

            var configCourseVM = new MapperConfiguration(cfg => cfg.CreateMap<Course, CourseVM>());
            var mapperCourseVM = new Mapper(configCourseVM);

            var viewModel = new TopicIndexVM()
            {
                Topics = topics.Select(i => new TopicVM()
                {
                    Id = i.Id,
                    Author = i.Author,
                    CourseId = i.CourseId,
                    Name = i.Name,
                    CreatedDate = i.CreatedDate,
                    Description = i.Description,
                    UpdatedDate = i.UpdatedDate,
                    Courses = courses.Select(i => mapperCourseVM.Map<CourseVM>(i)).ToList(),
                    CourseName = i.Course.Name,
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

            var configCourseVM = new MapperConfiguration(cfg => cfg.CreateMap<Course, CourseVM>());
            var mapperCourseVM = new Mapper(configCourseVM);

            var configTopicVM = new MapperConfiguration(cfg => cfg.CreateMap<Topic, TopicVM>());
            var mapperTopicVM = new Mapper(configCourseVM);

            if (id != 0 && id.HasValue)
            {
                var topic = repository.GetTopicById(id.Value);
                viewModel = mapperTopicVM.Map<TopicVM>(topic);
            }

            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            viewModel.Courses = courses.Select(i => mapperCourseVM.Map<CourseVM>(i)).ToList();

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Topic(TopicVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var courses = repository.GetAllCourses();

                var configCourseVM = new MapperConfiguration(cfg => cfg.CreateMap<Course, CourseVM>());
                var mapperCourseVM = new Mapper(configCourseVM);

                var configTopicVM = new MapperConfiguration(cfg => cfg.CreateMap<Topic, TopicVM>());
                var mapperTopicVM = new Mapper(configCourseVM);

                viewModel.Courses = courses.Select(i => mapperCourseVM.Map<CourseVM>(i)).ToList();

                if (viewModel.Id != 0)
                {
                    var topic = mapperTopicVM.Map<Topic>(viewModel);

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
                    var topic = mapperTopicVM.Map<Topic>(viewModel);

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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Topic, TopicVM>());
            var mapper = new Mapper(config);

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
                    Topics = topics.Select(i => mapper.Map<TopicVM>(i)).ToList()
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
            var configTopicVM = new MapperConfiguration(cfg => cfg.CreateMap<Topic, TopicVM>());
            var mapperTopicVM = new Mapper(configTopicVM);

            var configVideoVM = new MapperConfiguration(cfg => cfg.CreateMap<Video, VideoVM>());
            var mapperVideoVM = new Mapper(configVideoVM);

            var viewModel = new VideoVM();
            var topics = repository.GetAllTopics();

            if (id != 0 && id.HasValue)
            {
                var video = repository.GetVideoById(id.Value);
                viewModel = mapperVideoVM.Map<VideoVM>(video);
            }

            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            viewModel.Topics = topics.Select(i => mapperTopicVM.Map<TopicVM>(i)).ToList();

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Video(VideoVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var configTopicVM = new MapperConfiguration(cfg => cfg.CreateMap<Topic, TopicVM>());
                var mapperTopicVM = new Mapper(configTopicVM);

                var configVideoVM = new MapperConfiguration(cfg => cfg.CreateMap<VideoVM, Video>());
                var mapperVideoVM = new Mapper(configVideoVM);

                var topics = repository.GetAllTopics();
                viewModel.Topics = topics.Select(i => mapperTopicVM.Map<TopicVM>(i)).ToList();

                if (viewModel.Id != 0)
                {
                    var video = mapperVideoVM.Map<Video>(viewModel);

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
                    var video = mapperVideoVM.Map<Video>(viewModel);

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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Topic, TopicVM>());
            var mapper = new Mapper(config);

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
                    Topics = topics.Select(i => mapper.Map<TopicVM>(i)).ToList()
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
            var configTopicVM = new MapperConfiguration(cfg => cfg.CreateMap<Topic, TopicVM>());
            var mapperTopicVM = new Mapper(configTopicVM);

            var configDefinitionVM = new MapperConfiguration(cfg => cfg.CreateMap<Definition, DefinitionVM>());
            var mapperDefinitionVM = new Mapper(configDefinitionVM);

            var viewModel = new DefinitionVM();
            var topics = repository.GetAllTopics();

            if (id != 0 && id.HasValue)
            {
                var video = repository.GetDefinitionById(id.Value);
                viewModel = mapperDefinitionVM.Map<DefinitionVM>(video);
            }

            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            viewModel.Topics = topics.Select(i => mapperTopicVM.Map<TopicVM>(i)).ToList();

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Definition(DefinitionVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var configTopicVM = new MapperConfiguration(cfg => cfg.CreateMap<Topic, TopicVM>());
                var mapperTopicVM = new Mapper(configTopicVM);

                var configDefinitionVM = new MapperConfiguration(cfg => cfg.CreateMap<DefinitionVM, Definition>());
                var mapperDefinitionVM = new Mapper(configDefinitionVM);

                var topics = repository.GetAllTopics();
                viewModel.Topics = topics.Select(i => mapperTopicVM.Map<TopicVM>(i)).ToList();

                if (viewModel.Id != 0)
                {
                    var definition = mapperDefinitionVM.Map<Definition>(viewModel);

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
                    var definition = mapperDefinitionVM.Map<Definition>(viewModel);

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
            var configCultureVM = new MapperConfiguration(cfg => cfg.CreateMap<Culture, CultureVM>());
            var mapperCultureVM = new Mapper(configCultureVM);

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
                    Cultures = cultures.Select(i => mapperCultureVM.Map<CultureVM>(i)).ToList()
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
            var configCultureVM = new MapperConfiguration(cfg => cfg.CreateMap<Culture, CultureVM>());
            var mapperCultureVM = new Mapper(configCultureVM);

            var configResourceVM = new MapperConfiguration(cfg => cfg.CreateMap<Resource, ResourceVM>());
            var mapperResourceVM = new Mapper(configResourceVM);

            var viewModel = new ResourceVM();
            var cultures = repository.GetAllCultures();

            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            viewModel.Cultures = cultures.Select(i => mapperCultureVM.Map<CultureVM>(i)).ToList();

            if (id != 0 && id.HasValue)
            {
                var resource = repository.GetResourceById(id.Value);
                mapperResourceVM.Map<ResourceVM>(resource);
            }

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Resource(ResourceVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var configCultureVM = new MapperConfiguration(cfg => cfg.CreateMap<Culture, CultureVM>());
                var mapperCultureVM = new Mapper(configCultureVM);

                var configResourceVM = new MapperConfiguration(cfg => cfg.CreateMap<ResourceVM, Resource>());
                var mapperResourceVM = new Mapper(configResourceVM);

                var cultures = repository.GetAllCultures();
                viewModel.Cultures = cultures.Select(i => mapperCultureVM.Map<CultureVM>(i)).ToList();

                if (viewModel.Id != 0)
                {
                    var resource = mapperResourceVM.Map<Resource>(viewModel);

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
                    var resource = mapperResourceVM.Map<Resource>(viewModel);

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

        #region Roles
        [HttpGet]
        public IActionResult RoleIndex(MessageVM message)
        {
            var roles = repository.GetAllRoles();

            var viewModel = new RoleIndexVM()
            {
                Roles = roles.Select(i => new RoleVM()
                {
                    Id = i.Id,
                    Name = i.Name,
                }).ToList()
            };

            if (message != null)
            {
                viewModel.Message = message;
            }

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Role(int? id)
        {
            var viewModel = new RoleVM();
            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            if (id != 0 && id.HasValue)
            {
                var role = repository.GetRoleById(id.Value);
                viewModel.Id = id.Value;
                viewModel.Name = role.Name;
            }

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Role(RoleVM viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Id != 0)
                {
                    var role = new Role();
                    role.Name = viewModel.Name;
                    var resultUpdate = repository.UpdateRole(viewModel.Id, role);
                    if (resultUpdate)
                    {
                        return RedirectToAction("RoleIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = "Rollar muvofaqiyatli o'zgartirildi"
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = "Rolni o'zgartirib bo'lmaydi"
                        };
                    }
                }
                else
                {
                    var role = new Role();
                    role.Name = viewModel.Name;
                    var resultAdd = repository.AddRole(role);
                    if (resultAdd)
                    {
                        return RedirectToAction("RoleIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = "Rolelar muvofaqiyatli yaratildi"
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = "Roleni yaratib bo'lmaydi"
                        };
                    }
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteRole(int id)
        {
            var result = repository.DeleteRole(id);
            if (result)
            {
                return RedirectToAction("RoleIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Success,
                    MessageText = "Rolelar muvofaqiyatli o'chirildi"
                });
            }
            else
            {

                return RedirectToAction("RoleIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = "Rolelar o'chirilishi bilan problema bor"
                });
            }
        }
        #endregion
    }
}
