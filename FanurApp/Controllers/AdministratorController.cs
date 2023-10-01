using Microsoft.AspNetCore.Mvc;

namespace FanurApp.Controllers
{
    public class AdministratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
