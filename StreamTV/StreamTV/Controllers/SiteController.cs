using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StreamTV.Controllers
{
    public class SiteController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/ForgotPassword")]
        public IActionResult Forgot()
        {
            return View();
        }

        [Route("/Contact")]
        public IActionResult Contact()
        {
            return View();
        }
    }
}