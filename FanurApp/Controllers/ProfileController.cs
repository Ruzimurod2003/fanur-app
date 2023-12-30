using Microsoft.AspNetCore.Mvc;

namespace FanurApp.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
