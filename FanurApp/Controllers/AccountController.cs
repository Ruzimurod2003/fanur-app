using FanurApp.Repositories;
using FanurApp.ViewModels.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using FanurApp.Models;
using FanurApp.Commons.Enums;

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = repository.RegisterUser(viewModel);
                if (result != null)
                {
                    await Authenticate(result);

                    if (result.RoleId == (int)RolesEnum.Administrator)
                        return RedirectToAction("Index", "Administrator", null);
                    else if (result.RoleId == (int)RolesEnum.Teacher)
                        return RedirectToAction("Index", "Teacher");
                    else
                        return RedirectToAction("Index", "Student");
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = repository.LoginUser(viewModel);

                if (result != null)
                {
                    await Authenticate(result);

                    if (result.RoleId == (int)RolesEnum.Administrator)
                        return RedirectToAction("Index", "Administrator", null);
                    else if (result.RoleId == (int)RolesEnum.Teacher)
                        return RedirectToAction("Index", "Teacher");
                    else
                        return RedirectToAction("Index", "Student");
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
        [ValidateAntiForgeryToken]
        public IActionResult Forget(ForgetVM viewModel)
        {

            return View(viewModel);
        }
        public IActionResult Reset()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reset(ResetVM viewModel)
        {

            return View(viewModel);
        }
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name),
                new Claim("Full_Name", user.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
