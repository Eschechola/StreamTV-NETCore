using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StreamTV.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Docs()
        {
            return View();
        }

        public IActionResult UpdateAccount()
        {
            return View();
        }

        public IActionResult EditTelevision(int idTelevision = 0)
        {
            return View();
        }

        public IActionResult AddTelevision()
        {
            return View();
        }
    }
}
