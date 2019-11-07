using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamTV.Application;
using StreamTV.Context;
using StreamTV.Models;

namespace StreamTV.Controllers
{
    public class HomeController : Controller
    {
        //variavel de contexto
        private DatabaseContext _context = new DatabaseContext();

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            var dadosCliente = new Cliente();
            try
            {
                var idCliente = Int32.Parse(User.Claims.ToList()[3].Value);
                dadosCliente = new ClienteApplication(_context).GetById(idCliente);
            }
            catch (Exception)
            {
                ViewBag.Erro = "Não foi possível se comunicar com a base de dados";
            }

            return View(dadosCliente);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            Response.Redirect("/");
            return View();
        }

        public IActionResult Docs()
        {
            return View();
        }

        [Authorize]
        public IActionResult UpdateAccount()
        {
            return View();
        }

        [Authorize]
        [Route("/Home/EditTelevision/{idTelevision}")]
        [Route("/Home/EditTelevision")]
        public IActionResult EditTelevision(int idTelevision = 0)
        {
            return View();
        }

        [Authorize]
        public IActionResult AddTelevision()
        {
            return View();
        }

        [Route("NotFoundPage")]
        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
