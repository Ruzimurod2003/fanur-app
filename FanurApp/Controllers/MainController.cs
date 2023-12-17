using FanurApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FanurApp.Controllers
{
    public class MainController : Controller
    {
        private readonly IMainRepository repository;

        public MainController(IMainRepository _repository)
        {
            repository = _repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Course(int id)
        {
            var data = repository.GetCourseById(id);
            return View(data);
        }
    }
}
