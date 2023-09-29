﻿using FanurApp.Exceptions;
using FanurApp.Repositories;
using FanurApp.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace FanurApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository repository;

        public AccountController(IAccountRepository _repository)
        {
            repository = _repository;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterVM viewModel)
        {
            try
            {
                var result = repository.RegisterUser(viewModel);

                return RedirectToAction("Index", "Home", null);
            }
            catch (AccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(viewModel);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = repository.LoginUser(viewModel);

                    return RedirectToAction("Index", "Home", null);
                }
            }
            catch (AccountException ex)
            {
                Console.WriteLine(ex.Message);
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
