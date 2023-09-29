using FanurApp.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace FanurApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterVM viewModel)
        {

            return View(viewModel);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM viewModel)
        {

            return View(viewModel);
        }
        public IActionResult Forget()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Forget(ForgetVM viewModel)
        {

            return View(viewModel);
        }
        public IActionResult Reset()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Reset(ResetVM viewModel)
        {

            return View(viewModel);
        }
    }
}
