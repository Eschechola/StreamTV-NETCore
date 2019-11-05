using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StreamTV.Controllers
{
    public class NotFoundPageController : Controller
    {
        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}