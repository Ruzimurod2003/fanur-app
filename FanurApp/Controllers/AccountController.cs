using FanurApp.Repositories;
using FanurApp.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace FanurApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository repository;
        private readonly IStringLocalizer localizer;

        public AccountController(IAccountRepository _repository, IStringLocalizer _localizer)
        {
            repository = _repository;
            localizer = _localizer;
        }
        public IActionResult Register()
        {
            RegisterVM viewModel = new RegisterVM();
            viewModel.ErrorMessage = string.Empty;
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Register(RegisterVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = repository.RegisterUser(viewModel);
                if (result != null)
                {
                    return RedirectToAction("Index", "Home", null);
                }
                viewModel.ErrorMessage = localizer["this_user_is_already_registered"];
            }
            return View(viewModel);
        }
        public IActionResult Login()
        {
            LoginVM viewModel = new LoginVM();
            viewModel.ErrorMessage = string.Empty;
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Login(LoginVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = repository.LoginUser(viewModel);

                if (result != null)
                {
                    return RedirectToAction("Index", "Home", null);
                }
                viewModel.ErrorMessage = localizer["you_entered_the_wrong_password_or_email"];
            }
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
