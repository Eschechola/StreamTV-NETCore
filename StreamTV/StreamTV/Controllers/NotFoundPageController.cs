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