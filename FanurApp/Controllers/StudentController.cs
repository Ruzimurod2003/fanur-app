﻿using Microsoft.AspNetCore.Mvc;

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
    }
}
