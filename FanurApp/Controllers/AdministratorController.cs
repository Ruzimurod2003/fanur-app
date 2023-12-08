using FanurApp.Commons.Enums;
using FanurApp.Models;
using FanurApp.Repositories;
using FanurApp.ViewModels;
using FanurApp.ViewModels.Administrator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace FanurApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        private readonly IAdministratorRepository repository;
        private readonly IStringLocalizer localizer;
        private readonly IWebHostEnvironment appEnvironment;

        public AdministratorController(
            IAdministratorRepository _repository,
            IStringLocalizer _localizer,
            IWebHostEnvironment _appEnvironment)
        {
            repository = _repository;
            localizer = _localizer;
            appEnvironment = _appEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region Files
        [HttpGet]
        public IActionResult FileIndex(MessageVM message)
        {
            var files = repository.GetAllFiles();
            var viewModel = new FileIndexVM()
            {
                Files = files.Select(i => new FileVM()
                {
                    Id = i.Id,
                    Name = i.Name,
                    FileTypeId = i.FileTypeId,
                    CreatedDate = i.CreatedDate,
                    Path = i.Path,
                    TopicId = i.TopicId,
                    TopicName = i.Topic.Name,
                    Description = i.Description
                }).ToList()
            };
            if (message != null)
            {
                viewModel.Message = message;
            }

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult File(int? id)
        {
            ViewBag.Topics = repository.GetAllTopics().Select(i => new TopicVM()
            {
                Id = i.Id,
                Name = i.Name
            });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> File(IFormFile uploadedFile, int topicId, string description)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string fileName = uploadedFile.FileName;
                var path = Path.Combine(appEnvironment.WebRootPath, "files");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                Models.File file = new Models.File { Name = uploadedFile.FileName, Path = path, TopicId = topicId };
                if (uploadedFile.ContentType == "audio/x-m4a")
                {
                    file.FileTypeId = (int)FileTypesEnum.Audio;
                }
                else if (uploadedFile.ContentType == "image/jpeg")
                {
                    file.FileTypeId = (int)FileTypesEnum.Image;
                }
                file.Description = description;
                bool result = repository.AddFile(file);
                if (result)
                {
                    return RedirectToAction("FileIndex", "Administrator", new MessageVM()
                    {
                        MessageType = (int)MessageTypesEnum.Success,
                        MessageText = localizer["files_created_successfully"]
                    });
                }
                else
                {
                    return RedirectToAction("FileIndex", "Administrator", new MessageVM()
                    {
                        MessageType = (int)MessageTypesEnum.Danger,
                        MessageText = localizer["files_created_unsuccessfully"]
                    });
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult DeleteFile(int id)
        {
            var result = repository.DeleteFile(id);
            if (result)
            {
                return RedirectToAction("FileIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Success,
                    MessageText = localizer["files_deleted_successfully"]
                });
            }
            else
            {

                return RedirectToAction("FileIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = localizer["files_deleted_unsuccessfully"]
                });
            }
        }
        #endregion

        #region Users
        [HttpGet]
        public IActionResult UserIndex(MessageVM message)
        {
            var users = repository.GetAllUsers();
            var roles = repository.GetAllRoles();

            var viewModel = new UserIndexVM()
            {
                Users = users.Select(i => new UserVM()
                {
                    Id = i.Id,
                    Email = i.Email,
                    Password = i.Password,
                    Name = i.Name,
                    CreatedDate = i.CreatedDate,
                    UpdatedDate = i.UpdatedDate,
                    RoleId = i.RoleId,
                    Roles = roles.Select(i => new RoleVM()
                    {
                        Id = i.Id,
                        Name = i.Name,
                    }).ToList(),
                    RoleName = i.Role.Name,
                }).ToList()
            };

            if (message != null)
            {
                viewModel.Message = message;
            }

            return View(viewModel);
        }
        [HttpGet]
        public new IActionResult User(int? id)
        {
            var viewModel = new UserVM();
            var roles = repository.GetAllRoles();

            if (id != 0 && id.HasValue)
            {
                var user = repository.GetUserById(id.Value);
                viewModel = new UserVM()
                {
                    Id = id.Value,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    CreatedDate = user.CreatedDate,
                    UpdatedDate = user.UpdatedDate,
                    RoleId = user.Role.Id,
                    RoleName = user.Role.Name,
                    IsAccepted = user.IsAccepted
                };
            }

            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            viewModel.Roles = roles.Select(i => new RoleVM()
            {
                Id = i.Id,
                Name = i.Name
            }).ToList();

            return View(viewModel);
        }
        [HttpPost]
        public new IActionResult User(UserVM viewModel)
        {
            var roles = repository.GetAllRoles();

            viewModel.Roles = roles.Select(i => new RoleVM()
            {
                Id = i.Id,
                Name = i.Name
            }).ToList();

            if (ModelState.IsValid)
            {
                if (viewModel.Id != 0)
                {
                    var user = new User()
                    {
                        Id = viewModel.Id,
                        Name = viewModel.Name,
                        Email = viewModel.Email,
                        Password = viewModel.Password,
                        RoleId = viewModel.RoleId,
                        IsAccepted = viewModel.IsAccepted,
                        CreatedDate = viewModel.CreatedDate,
                        UpdatedDate = viewModel.UpdatedDate
                    };

                    var resultUpdate = repository.UpdateUser(viewModel.Id, user);
                    if (resultUpdate)
                    {
                        return RedirectToAction("UserIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = localizer["users_updated_successfully"]
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = localizer["users_updated_unsuccessfully"]
                        };
                    }
                }
                else
                {
                    var user = new User()
                    {
                        Id = viewModel.Id,
                        Name = viewModel.Name,
                        Email = viewModel.Email,
                        Password = viewModel.Password,
                        RoleId = viewModel.RoleId,
                        IsAccepted = viewModel.IsAccepted,
                        CreatedDate = viewModel.CreatedDate,
                        UpdatedDate = viewModel.UpdatedDate
                    };

                    var resultAdd = repository.AddUser(user);
                    if (resultAdd)
                    {
                        return RedirectToAction("UserIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = localizer["users_created_successfully"]
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = localizer["users_created_unsuccessfully"]
                        };
                    }
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            var result = repository.DeleteUser(id);
            if (result)
            {
                return RedirectToAction("UserIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Success,
                    MessageText = localizer["users_deleted_successfully"]
                });
            }
            else
            {
                return RedirectToAction("UserIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = localizer["users_deleted_unsuccessfully"]
                });
            }
        }
        #endregion

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
                    Name = i.Name,
                    Author = i.Author,
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

            if (id != 0 && id.HasValue)
            {
                var course = repository.GetCourseById(id.Value);

                viewModel = new CourseVM()
                {
                    Id = id.Value,
                    Name = course.Name,
                    Author = course.Author,
                    CreatedDate = course.CreatedDate,
                    Description = course.Description,
                    UpdatedDate = course.UpdatedDate
                };
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
                    var course = new Course()
                    {
                        Id = viewModel.Id,
                        Name = viewModel.Name,
                        Author = viewModel.Author,
                        CreatedDate = viewModel.CreatedDate,
                        Description = viewModel.Description,
                        UpdatedDate = viewModel.UpdatedDate
                    };

                    var resultUpdate = repository.UpdateCourse(viewModel.Id, course);
                    if (resultUpdate)
                    {
                        return RedirectToAction("CourseIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = localizer["courses_updated_successfully"]
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = localizer["courses_updated_unsuccessfully"]
                        };
                    }
                }
                else
                {
                    var course = new Course()
                    {
                        Name = viewModel.Name,
                        Author = viewModel.Author,
                        CreatedDate = viewModel.CreatedDate,
                        Description = viewModel.Description,
                        UpdatedDate = viewModel.UpdatedDate
                    };

                    var resultAdd = repository.AddCourse(course);
                    if (resultAdd)
                    {
                        return RedirectToAction("CourseIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = localizer["courses_created_successfully"]
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = localizer["courses_created_unsuccessfully"]
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
                    MessageText = localizer["courses_deleted_successfully"]
                });
            }
            else
            {
                return RedirectToAction("CourseIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = localizer["courses_deleted_unsuccessfully"]
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
                    Name = i.Name,
                    CreatedDate = i.CreatedDate,
                    Description = i.Description,
                    UpdatedDate = i.UpdatedDate,
                    Courses = courses.Select(i => new CourseVM()
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Description = i.Description,
                        Author = i.Author,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate
                    }).ToList(),
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

            if (id != 0 && id.HasValue)
            {
                var topic = repository.GetTopicById(id.Value);
                viewModel = new TopicVM()
                {
                    Id = id.Value,
                    Name = topic.Name,
                    Description = topic.Description,
                    Author = topic.Author,
                    CreatedDate = topic.CreatedDate,
                    UpdatedDate = topic.UpdatedDate,
                    CourseId = topic.Course.Id,
                    CourseName = topic.Course.Name
                };
            }

            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            viewModel.Courses = courses.Select(i => new CourseVM()
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Author = i.Author,
                CreatedDate = i.CreatedDate,
                UpdatedDate = i.UpdatedDate
            }).ToList();

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Topic(TopicVM viewModel)
        {
            var courses = repository.GetAllCourses();

            viewModel.Courses = courses.Select(i => new CourseVM()
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Author = i.Author,
                CreatedDate = i.CreatedDate,
                UpdatedDate = i.UpdatedDate
            }).ToList();

            if (ModelState.IsValid)
            {
                if (viewModel.Id != 0)
                {
                    var topic = new Topic()
                    {
                        Id = viewModel.Id,
                        Name = viewModel.Name,
                        Description = viewModel.Description,
                        Author = viewModel.Author,
                        CourseId = viewModel.CourseId,
                        CreatedDate = viewModel.CreatedDate,
                        UpdatedDate = viewModel.UpdatedDate
                    };

                    var resultUpdate = repository.UpdateTopic(viewModel.Id, topic);
                    if (resultUpdate)
                    {
                        return RedirectToAction("TopcIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = localizer["topics_updated_successfully"]
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = localizer["topics_updated_unsuccessfully"]
                        };
                    }
                }
                else
                {
                    var topic = new Topic()
                    {
                        Id = viewModel.Id,
                        Name = viewModel.Name,
                        Description = viewModel.Description,
                        Author = viewModel.Author,
                        CourseId = viewModel.CourseId,
                        CreatedDate = viewModel.CreatedDate,
                        UpdatedDate = viewModel.UpdatedDate
                    };

                    var resultAdd = repository.AddTopic(topic);
                    if (resultAdd)
                    {
                        return RedirectToAction("TopicIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = localizer["topics_created_successfully"]
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = localizer["topics_created_unsuccessfully"]
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
                    MessageText = localizer["topics_deleted_successfully"]
                });
            }
            else
            {
                return RedirectToAction("TopicIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = localizer["topics_deleted_unsuccessfully"]
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
                    Topics = topics.Select(i => new TopicVM()
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Description = i.Description,
                        Author = i.Author,
                        CreatedDate = i.CreatedDate,
                        CourseId = i.CourseId,
                        UpdatedDate = i.UpdatedDate,
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

            var topics = (viewModel.CourseId != 0) ?
                            repository.GetAllTopicsByCourseId(viewModel.CourseId) :
                            repository.GetAllTopics();

            var courses = repository.GetAllCourses();

            if (id != 0 && id.HasValue)
            {
                var video = repository.GetVideoById(id.Value);
                viewModel = new VideoVM()
                {
                    Id = video.Id,
                    URLName = video.URLName,
                    UpdatedDate = video.UpdatedDate,
                    Caption = video.Caption,
                    CreatedDate = video.CreatedDate,
                    Author = video.Author,
                    TopicId = video.TopicId,
                    TopicName = video.Topic.Name,
                };
            }

            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            viewModel.Topics = topics.Select(i => new TopicVM()
            {
                Id = i.Id,
                Name = i.Name,
                Author = i.Author,
                CreatedDate = i.CreatedDate,
                UpdatedDate = i.UpdatedDate,
                CourseId = i.CourseId,
                Description = i.Description,
                CourseName = i.Course.Name
            }).ToList();

            viewModel.Courses = courses.Select(i => new CourseVM()
            {
                Id = i.Id,
                Name = i.Name,
                Author = i.Author,
                CreatedDate = i.CreatedDate,
                UpdatedDate = i.UpdatedDate,
                Description = i.Description
            }).ToList();

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Video(VideoVM viewModel)
        {
            var topics = (viewModel.CourseId != 0) ?
                            repository.GetAllTopicsByCourseId(viewModel.CourseId) :
                            repository.GetAllTopics();

            viewModel.Topics = topics.Select(i => new TopicVM()
            {
                Id = i.Id,
                Name = i.Name,
                Author = i.Author,
                CreatedDate = i.CreatedDate,
                UpdatedDate = i.UpdatedDate,
                CourseId = i.CourseId,
                Description = i.Description,
                CourseName = i.Course.Name
            }).ToList();

            var courses = repository.GetAllCourses();
            viewModel.Courses = courses.Select(i => new CourseVM()
            {
                Id = i.Id,
                Name = i.Name,
                Author = i.Author,
                CreatedDate = i.CreatedDate,
                UpdatedDate = i.UpdatedDate,
                Description = i.Description
            }).ToList();

            if (ModelState.IsValid)
            {
                if (viewModel.Id != 0)
                {
                    var video = new Video()
                    {
                        Id = viewModel.Id,
                        URLName = viewModel.URLName,
                        UpdatedDate = viewModel.UpdatedDate,
                        CreatedDate = viewModel.CreatedDate,
                        Author = viewModel.Author,
                        Caption = viewModel.Caption,
                        TopicId = viewModel.TopicId
                    };

                    var resultUpdate = repository.UpdateVideo(viewModel.Id, video);

                    if (resultUpdate)
                    {
                        return RedirectToAction("VideoIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = localizer["videos_updated_successfully"]
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = localizer["videos_updated_unsuccessfully"]
                        };
                    }
                }
                else
                {
                    var video = new Video()
                    {
                        TopicId = viewModel.TopicId,
                        URLName = viewModel.URLName,
                        Caption = viewModel.Caption,
                        Author = viewModel.Author,
                        CreatedDate = viewModel.CreatedDate,
                        UpdatedDate = viewModel.UpdatedDate
                    };

                    var resultAdd = repository.AddVideo(video);

                    if (resultAdd)
                    {
                        return RedirectToAction("VideoIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = localizer["videos_created_successfully"]
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = localizer["videos_created_unsuccessfully"]
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
                    MessageText = localizer["videos_deleted_successfully"]
                });
            }
            else
            {

                return RedirectToAction("VideoIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = localizer["videos_deleted_unsuccessfully"]
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
                    Topics = topics.Select(i => new TopicVM()
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Author = i.Author,
                        Description = i.Description,
                        UpdatedDate = i.UpdatedDate,
                        CreatedDate = i.CreatedDate,
                        CourseId = i.CourseId,
                        CourseName = i.Course.Name
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
            var topics = (viewModel.CourseId != 0) ?
                            repository.GetAllTopicsByCourseId(viewModel.CourseId) :
                            repository.GetAllTopics();
            var courses = repository.GetAllCourses();

            if (id != 0 && id.HasValue)
            {
                var video = repository.GetDefinitionById(id.Value);
                viewModel = new DefinitionVM()
                {
                    Id = video.Id,
                    Author = video.Author,
                    CreatedDate = video.CreatedDate,
                    UpdatedDate = video.UpdatedDate,
                    HMTLText = video.HMTLText,
                    TopicId = video.TopicId,
                    TopicName = video.Topic.Name
                };
            }

            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            viewModel.Topics = topics.Select(i => new TopicVM()
            {
                Id = i.Id,
                Name = i.Name,
                Author = i.Author,
                Description = i.Description,
                CreatedDate = i.CreatedDate,
                UpdatedDate = i.UpdatedDate,
                CourseId = i.CourseId,
                CourseName = i.Course.Name
            }).ToList();

            viewModel.Courses = courses.Select(i => new CourseVM()
            {
                Id = i.Id,
                Name = i.Name,
                Author = i.Author,
                Description = i.Description,
                CreatedDate = i.CreatedDate,
                UpdatedDate = i.UpdatedDate
            }).ToList();

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Definition(DefinitionVM viewModel)
        {
            var topics = (viewModel.CourseId != 0) ?
                            repository.GetAllTopicsByCourseId(viewModel.CourseId) :
                            repository.GetAllTopics();
            var courses = repository.GetAllCourses();

            viewModel.Topics = topics.Select(i => new TopicVM()
            {
                Id = i.Id,
                Name = i.Name,
                Author = i.Author,
                Description = i.Description,
                CreatedDate = i.CreatedDate,
                UpdatedDate = i.UpdatedDate,
                CourseId = i.CourseId,
                CourseName = i.Course.Name
            }).ToList();

            viewModel.Courses = courses.Select(i => new CourseVM()
            {
                Id = i.Id,
                Name = i.Name,
                Author = i.Author,
                Description = i.Description,
                CreatedDate = i.CreatedDate,
                UpdatedDate = i.UpdatedDate
            }).ToList();

            if (ModelState.IsValid)
            {
                if (viewModel.Id != 0)
                {
                    var definition = new Definition()
                    {
                        Id = viewModel.Id,
                        HMTLText = viewModel.HMTLText,
                        Author = viewModel.Author,
                        TopicId = viewModel.TopicId,
                        CreatedDate = viewModel.CreatedDate,
                        UpdatedDate = viewModel.UpdatedDate
                    };

                    var resultUpdate = repository.UpdateDefinition(viewModel.Id, definition);
                    if (resultUpdate)
                    {
                        return RedirectToAction("DefinitionIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = localizer["definitions_updated_successfully"]
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = localizer["definitions_updated_unsuccessfully"]
                        };
                    }
                }
                else
                {
                    var definition = new Definition()
                    {
                        TopicId = viewModel.TopicId,
                        Author = viewModel.Author,
                        HMTLText = viewModel.HMTLText,
                        CreatedDate = viewModel.CreatedDate,
                        UpdatedDate = viewModel.UpdatedDate
                    };

                    var resultAdd = repository.AddDefinition(definition);
                    if (resultAdd)
                    {
                        return RedirectToAction("DefinitionIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = localizer["definitions_created_successfully"]
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = localizer["definitions_created_unsuccessfully"]
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
                    MessageText = localizer["definitions_deleted_successfully"]
                });
            }
            else
            {
                return RedirectToAction("DefinitionIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = localizer["definitions_deleted_unsuccessfully"]
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
                    Cultures = cultures.Select(i => new CultureVM()
                    {
                        Id = i.Id,
                        Name = i.Name
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
            viewModel.Cultures = cultures.Select(i => new CultureVM()
            {
                Id = i.Id,
                Name = i.Name
            }).ToList();

            if (id != 0 && id.HasValue)
            {
                var resource = repository.GetResourceById(id.Value);
                viewModel.Id = resource.Id;
                viewModel.CultureName = resource.Culture.Name;
                viewModel.CreatedDate = resource.CreatedDate;
                viewModel.UpdatedDate = resource.UpdatedDate;
                viewModel.CultureId = resource.CultureId;
                viewModel.Value = resource.Value;
                viewModel.Key = resource.Key;
            }

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Resource(ResourceVM viewModel)
        {
            var cultures = repository.GetAllCultures();
            viewModel.Cultures = cultures.Select(i => new CultureVM()
            {
                Id = i.Id,
                Name = i.Name
            }).ToList();

            if (ModelState.IsValid)
            {
                if (viewModel.Id != 0)
                {
                    var resource = new Resource()
                    {
                        Id = viewModel.Id,
                        Key = viewModel.Key,
                        Value = viewModel.Value,
                        CultureId = viewModel.CultureId,
                        CreatedDate = viewModel.CreatedDate,
                        UpdatedDate = viewModel.UpdatedDate
                    };

                    var resultUpdate = repository.UpdateResource(viewModel.Id, resource);
                    if (resultUpdate)
                    {
                        return RedirectToAction("ResourceIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = localizer["resources_updated_successfully"]
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = localizer["resources_updated_unsuccessfully"]
                        };
                    }
                }
                else
                {
                    var resource = new Resource()
                    {
                        Id = viewModel.Id,
                        Key = viewModel.Key,
                        Value = viewModel.Value,
                        CultureId = viewModel.CultureId,
                        CreatedDate = viewModel.CreatedDate,
                        UpdatedDate = viewModel.UpdatedDate
                    };

                    var resultAdd = repository.AddResource(resource);
                    if (resultAdd)
                    {
                        return RedirectToAction("ResourceIndex", "Administrator", new MessageVM()
                        {
                            MessageType = (int)MessageTypesEnum.Success,
                            MessageText = localizer["resources_created_successfully"]
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = localizer["resources_created_unsuccessfully"]
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
                    MessageText = localizer["resources_deleted_successfully"]
                });
            }
            else
            {
                return RedirectToAction("ResourceIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = localizer["resources_deleted_unsuccessfully"]
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
                            MessageText = localizer["roles_updated_successfully"]
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = localizer["roles_updated_unsuccessfully"]
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
                            MessageText = localizer["roles_created_successfully"]
                        });
                    }
                    else
                    {
                        viewModel.ErrorMessage = new MessageVM
                        {
                            MessageType = (int)MessageTypesEnum.Danger,
                            MessageText = localizer["roles_created_unsuccessfully"]
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
                    MessageText = localizer["roles_deleted_successfully"]
                });
            }
            else
            {
                return RedirectToAction("RoleIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = localizer["roles_deleted_unsuccessfully"]
                });
            }
        }
        #endregion        

        #region Quizzess
        [HttpGet]
        public IActionResult QuizIndex(MessageVM message)
        {
            var quizzes = repository.GetAllQuizzes();
            var topics = repository.GetAllTopics();

            var viewModel = new QuizIndexVM()
            {
                Quizzes = quizzes.Select(i => new QuizVM()
                {
                    Id = i.Id,
                    TopicId = i.TopicId,
                    TopicName = i.Topic.Name,
                    CreatedDate = i.CreatedDate,
                    UpdatedDate = i.UpdatedDate,
                    AnswerA = i.AnswerA,
                    AnswerB = i.AnswerB,
                    AnswerC = i.AnswerC,
                    AnswerD = i.AnswerD,
                    IsTrueAnswer = i.IsTrueAnswer,
                    QuestionText = i.QuestionText,
                    Topics = topics.Select(i => new TopicVM()
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Description = i.Description,
                        Author = i.Author,
                        CreatedDate = i.CreatedDate,
                        CourseId = i.CourseId,
                        UpdatedDate = i.UpdatedDate,
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
        public IActionResult Quiz(int? id)
        {
            var viewModel = new QuizVM();
            var topics = (viewModel.CourseId != 0) ?
                            repository.GetAllTopicsByCourseId(viewModel.CourseId) :
                            repository.GetAllTopics();
            var courses = repository.GetAllCourses();

            if (id != 0 && id.HasValue)
            {
                var quiz = repository.GetQuizById(id.Value);
                viewModel = new QuizVM()
                {
                    Id = quiz.Id,
                    UpdatedDate = quiz.UpdatedDate,
                    CreatedDate = quiz.CreatedDate,
                    AnswerA = quiz.AnswerA,
                    AnswerB = quiz.AnswerB,
                    AnswerC = quiz.AnswerC,
                    AnswerD = quiz.AnswerD,
                    IsTrueAnswer = quiz.IsTrueAnswer,
                    QuestionText = quiz.QuestionText,
                    TopicId = quiz.TopicId,
                    TopicName = quiz.Topic.Name,
                };
            }

            viewModel.ErrorMessage = new MessageVM
            {
                MessageType = (int)MessageTypesEnum.None,
                MessageText = string.Empty
            };
            viewModel.Topics = topics.Select(i => new TopicVM()
            {
                Id = i.Id,
                Name = i.Name,
                Author = i.Author,
                CreatedDate = i.CreatedDate,
                UpdatedDate = i.UpdatedDate,
                CourseId = i.CourseId,
                Description = i.Description,
                CourseName = i.Course.Name
            }).ToList();

            viewModel.Courses = courses.Select(i => new CourseVM()
            {
                Id = i.Id,
                Name = i.Name,
                Author = i.Author,
                CreatedDate = i.CreatedDate,
                UpdatedDate = i.UpdatedDate,
                Description = i.Description
            }).ToList();

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Quiz(QuizVM viewModel)
        {
            var topics = (viewModel.CourseId != 0) ?
                            repository.GetAllTopicsByCourseId(viewModel.CourseId) :
                            repository.GetAllTopics();
            var courses = repository.GetAllCourses();

            viewModel.Topics = topics.Select(i => new TopicVM()
            {
                Id = i.Id,
                Name = i.Name,
                Author = i.Author,
                CreatedDate = i.CreatedDate,
                UpdatedDate = i.UpdatedDate,
                CourseId = i.CourseId,
                Description = i.Description,
                CourseName = i.Course.Name
            }).ToList();

            viewModel.Courses = courses.Select(i => new CourseVM()
            {
                Id = i.Id,
                Name = i.Name,
                Author = i.Author,
                CreatedDate = i.CreatedDate,
                UpdatedDate = i.UpdatedDate,
                Description = i.Description
            }).ToList();

            if (ModelState.IsValid)
            {
                if (!Enum.IsDefined(typeof(QuizAnswersEnum), viewModel.IsTrueAnswer))
                {
                    ModelState.AddModelError("IsTrueAnswer", localizer["enter_one_of_the_answers_A_B_C_D_for_the_system"]);
                }
                else
                {
                    if (viewModel.Id != 0)
                    {
                        var quiz = new Quiz()
                        {
                            Id = viewModel.Id,
                            UpdatedDate = viewModel.UpdatedDate,
                            CreatedDate = viewModel.CreatedDate,
                            AnswerA = viewModel.AnswerA,
                            AnswerB = viewModel.AnswerB,
                            AnswerC = viewModel.AnswerC,
                            AnswerD = viewModel.AnswerD,
                            IsTrueAnswer = viewModel.IsTrueAnswer,
                            QuestionText = viewModel.QuestionText,
                            TopicId = viewModel.TopicId
                        };

                        var resultUpdate = repository.UpdateQuiz(viewModel.Id, quiz);

                        if (resultUpdate)
                        {
                            return RedirectToAction("QuizIndex", "Administrator", new MessageVM()
                            {
                                MessageType = (int)MessageTypesEnum.Success,
                                MessageText = localizer["quizzes_updated_successfully"]
                            });
                        }
                        else
                        {
                            viewModel.ErrorMessage = new MessageVM
                            {
                                MessageType = (int)MessageTypesEnum.Danger,
                                MessageText = localizer["quizzes_updated_unsuccessfully"]
                            };
                        }
                    }
                    else
                    {
                        var Quiz = new Quiz()
                        {
                            TopicId = viewModel.TopicId,
                            AnswerA = viewModel.AnswerA,
                            AnswerB = viewModel.AnswerB,
                            AnswerC = viewModel.AnswerC,
                            AnswerD = viewModel.AnswerD,
                            IsTrueAnswer = viewModel.IsTrueAnswer,
                            QuestionText = viewModel.QuestionText,
                            CreatedDate = viewModel.CreatedDate,
                            UpdatedDate = viewModel.UpdatedDate
                        };

                        var resultAdd = repository.AddQuiz(Quiz);

                        if (resultAdd)
                        {
                            return RedirectToAction("QuizIndex", "Administrator", new MessageVM()
                            {
                                MessageType = (int)MessageTypesEnum.Success,
                                MessageText = localizer["quizzes_created_successfully"]
                            });
                        }
                        else
                        {
                            viewModel.ErrorMessage = new MessageVM
                            {
                                MessageType = (int)MessageTypesEnum.Danger,
                                MessageText = localizer["quizzes_created_unsuccessfully"]
                            };
                        }
                    }
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteQuiz(int id)
        {
            var result = repository.DeleteQuiz(id);
            if (result)
            {
                return RedirectToAction("QuizIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Success,
                    MessageText = localizer["quizzes_deleted_successfully"]
                });
            }
            else
            {

                return RedirectToAction("QuizIndex", "Administrator", new MessageVM()
                {
                    MessageType = (int)MessageTypesEnum.Danger,
                    MessageText = localizer["quizzes_deleted_unsuccessfully"]
                });
            }
        }
        #endregion
    }
}
