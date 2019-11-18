using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamAPI.Applications;
using StreamAPI.Models;

namespace StreamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelevisionController : ControllerBase
    {
        //variavel de contexto
        private SenaitvsContext _context = new SenaitvsContext();

        Messages errorDatabase = new Messages
        {
            Message = "Não foi possível se comunicar com a base de dados!"
        };

        Messages errorNull = new Messages
        {
            Message = "Não foi encontrada nenhuma televisão."
        };

        [HttpPost]
        [Route("GetByCode")]
        public IActionResult GetByCode([FromBody]string code)
        {
            try
            {
                var televisao = new TelevisionApplication(_context).GetByCode(code);

                if (televisao != null)
                {
                    return Ok(televisao);
                }
                else
                {
                    return BadRequest(errorNull);
                }

            }
            catch (Exception)
            {
                return BadRequest(errorDatabase);
            }
        }
    }
}