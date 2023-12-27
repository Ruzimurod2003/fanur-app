using Microsoft.AspNetCore.Mvc;

namespace FanurApp.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Listening()
        {
            return View();
        }
        public IActionResult Video()
        {
            return View();
        }
        public IActionResult Quiz()
        {
            return View();
        }
        public IActionResult Introduction()
        {
            return View();
        }
    }
}
