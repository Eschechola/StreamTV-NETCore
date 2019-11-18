using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StreamAPI.Applications;
using StreamAPI.Models;

namespace StreamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
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

        //configuração pra json .net core 3
        JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };


        [HttpPost]
        [Route("GetAllByCode")]
        public IActionResult GetAllByCode([FromBody]string code)
        {
            try
            {
                var listaDeVideos = new VideosApplication(_context).GetAllByCode(code);

                if (listaDeVideos != null)
                {
                    var json = JsonConvert.SerializeObject(listaDeVideos, jsonSettings);

                    return Ok(json);
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