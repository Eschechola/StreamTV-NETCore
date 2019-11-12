using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using StreamTV.Application;
using StreamTV.Context;
using StreamTV.Models;
using StreamTV.Utilities.Files;
using StreamTV.Utilities.Identification;
using StreamTV.Utilities.Route;

namespace StreamTV.Controllers
{
    public class HomeController : Controller
    {
        //variavel de contexto
        private DatabaseContext _context = new DatabaseContext();

        //variavel para poder acessar a pasta wwwroot nas funçoes que necessitam de email
        //dependencia será injetada nas classes necessarias
        private PhysicalFileProvider _provedorDiretoriosArquivos = new PhysicalFileProvider(Directory.GetCurrentDirectory());

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
        [RequestFormLimits(MultipartBodyLengthLimit = 999999999)]
        public IActionResult AddTelevision()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 999999999)]
        public IActionResult AddTelevision(Televisoes televisao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //verifica se os arquivos mandados nao sao nulos
                    if (HttpContext.Request.Form.Files != null)
                    {
                        //pega o id do cliente que foi guardado no cookie
                        var _televisao = televisao;
                        _televisao.FkIdCliente = Int32.Parse(User.Claims.ToList()[3].Value);

                        //variavel para verificar se a televisao existe ou nao
                        Televisoes codigoExiste = new Televisoes();
                        //variavel para armazenar o codigo gerado
                        string codigoGerado = string.Empty;

                        do
                        {
                            codigoGerado = new Code().GenerateCode();
                            //gera o codigo
                            _televisao.Codigo = codigoGerado;
                            //caso o codigo existe irá retornar uma televisao, senao retorna nulo e termina o loop
                            codigoExiste = new TelevisoesApplication(_context).GetByCode(codigoGerado);

                        } while (codigoExiste != null);

                        //adiciona a televisão
                        var adicionarTV = new TelevisoesApplication(_context).Insert(televisao);

                        //pega todos os arquivos enviados via POST
                        var arquivos = HttpContext.Request.Form.Files;

                        //salva os arquivos na pasta wwwroot/Videos
                        var listaDeVideos = new Manipulation(_provedorDiretoriosArquivos).SalvarArquivos(arquivos);

                        //insere os dados dos 4 vídeos no banco de dados
                        if(listaDeVideos.Count < 4)
                        {
                            for(int i =0;i< listaDeVideos.Count;i++)
                            {
                                var videoAdicionar = new Videos
                                {
                                    FkIdTelevisao = new TelevisoesApplication(_context).GetByCode(codigoGerado).Id,
                                    Modificado = 1,
                                    Url = DefaultRoute.Route + listaDeVideos[i]
                                };

                                var inseridoSucesso = new VideosApplication(_context).InsertConfirm(videoAdicionar);

                                if (!inseridoSucesso)
                                {
                                    ViewBag.Info += "O vídeo de número " + i + 1 + "não pode ser inserido. Tente novamente.";
                                }
                            }
                        }
                        else
                        {
                            ViewBag.Info = "Arquivos inválidos, por favor tente novamente";
                        }
                    }
                    else
                    {
                        ViewBag.Info = "Arquivos inválidos, por favor tente novamente";
                    }
                }

                ViewBag.Info += "Televisão e vídeos inseridos com sucesso";
            }
            catch (Exception ex)
            {
                ViewBag.Info = ex.ToString();
            }

            return View();
        }

        [Route("NotFoundPage")]
        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
