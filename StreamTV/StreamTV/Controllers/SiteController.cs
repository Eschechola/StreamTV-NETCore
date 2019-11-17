using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamTV.Application;
using StreamTV.Context;
using StreamTV.Models;
using StreamTV.Utilities.Emails;

namespace StreamTV.Controllers
{
    public class SiteController : Controller
    {
        //variavel de contexto
        private DatabaseContext _context = new DatabaseContext();

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("/Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Home/Index");
            }

            return View();
        }

        [AllowAnonymous]
        [Route("/Login")]
        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var cliente = new Cliente
            {
                Email = email,
                Senha = senha
            };

            var clienteRetornado = LoginCliente(cliente);

            if (clienteRetornado != null)
            {
                LoginIdentity(clienteRetornado);

                return Redirect("/Home/Index");
            }
            else
            {
                ViewBag.Info = "Email e / ou senha não encontrados";
            }

            return View();
        }

        private async void LoginIdentity(Cliente cliente)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, cliente.Instituicao),
                new Claim(ClaimTypes.Email, cliente.Email),
                new Claim(ClaimTypes.Role, "Cliente"),
                new Claim("IdCliente", cliente.Id.ToString()),
            };

            var identidadeDeUsuario = new ClaimsIdentity(claims, "Login");

            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

            var propriedadesDeAutenticacao = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(48),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesDeAutenticacao);
        }

        private Cliente LoginCliente(Cliente cliente)
        {
            try
            {
                var clienteRetornado = new ClienteApplication(_context).GetByLogin(cliente);
                return clienteRetornado;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [AllowAnonymous]
        [Route("/ForgotPassword")]
        public IActionResult Forgot()
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Home/Index");
            }

            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("/ForgotPassword")]
        public IActionResult Forgot(string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Home/Index");
            }

            try
            {
                var usuario = new ClienteApplication(_context).GetByEmail(email);
                
                if (usuario != null)
                {
                    var emailEnviado = new Messages(email).ForgotPassword();
                    ViewBag.Info = "Email de recuperação enviado com sucesso!!";
                }
                else
                {
                    ViewBag.Info = "Usuário não cadastrado na nossa base de dados";
                }

            }
            catch (Exception)
            {
                ViewBag.Info = "Aconteceu algum erro, por - favor tente novamente";
            }

            return View();
        }

        [Route("/Contact")]
        public IActionResult Contact()
        {
            return View();
        }
    }
}