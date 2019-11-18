using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamAPI.Models;

namespace StreamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            var mensagem = new Messages
            {
                Message = "Index StreamTV API"
            };

            return Ok(mensagem);
        }
    }
}