using FanurApp.Enums;
using FanurApp.Models;
using FanurApp.Repositories;
using FanurApp.ViewModels;
using FanurApp.ViewModels.Administrator;
using Microsoft.AspNetCore.Mvc;

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
    }
}
