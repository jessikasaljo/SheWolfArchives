﻿using Microsoft.AspNetCore.Mvc;

namespace SheWolf.Frontend.Controllers
{
    public class AuthorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
