﻿using Microsoft.AspNetCore.Mvc;

namespace FanurApp.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
