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

        //variavel que globalmente guardará o id do usuario
        private static int idUsuario;

        [Authorize]
        public IActionResult Index()
        {
            //sempre que iniciar a pagina jogara o id do usuario pra variavel global
            idUsuario = Int32.Parse(User.Claims.ToList()[3].Value);

            //instancia a lista que será retornada
            var listaDeTelevisoes = new List<Televisoes>();
            try
            {
                //pega todas as televisoes do usuario atraves do id dele
                listaDeTelevisoes = new TelevisoesApplication(_context).GetAllByIdUser(idUsuario);

                //se não conseguir pegar nenhuma televisao, informa a mensagem
                if (listaDeTelevisoes.Equals(null))
                {
                    ViewBag.Info = "Nenhuma televisão cadastrada.";
                }
            }
            catch (Exception)
            {
                ViewBag.Info = "Não foi possível se comunicar com a base de dados";
            }

            return View(listaDeTelevisoes);
        }

        [Authorize]
        public IActionResult Profile()
        {
            //instancia as variaveis de dados do usuario e lista de televisoes
            var dadosUsuario = new Cliente();
            var listaDeTelevisoes = new List<Televisoes>();
            try
            {
                //atribui a variavel de dados usuario com os dados no banco
                dadosUsuario = new ClienteApplication(_context).GetById(idUsuario);

                //atribui a lista de televisoes de dados usuario com os dados no banco
                listaDeTelevisoes = new TelevisoesApplication(_context).GetAllByIdUser(idUsuario);

                ViewBag.Televisoes = listaDeTelevisoes;
                ViewBag.QtTelevisoes = listaDeTelevisoes.Count;
            }
            catch (Exception)
            {
                ViewBag.Info = "Não foi possível se comunicar com a base de dados";
            }

            return View(dadosUsuario);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            //apaga os dados do cookie e retorna pra index do site
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
            var dadosUsuario = new Cliente();
            try
            {
                //pega os dados do usuario atraves do id e retorna eles para o formulario de alteraçao
                dadosUsuario = new ClienteApplication(_context).GetById(idUsuario);
                ViewBag.QtTelevisoes = new TelevisoesApplication(_context).GetAllByIdUser(idUsuario).Count;
            }
            catch (Exception)
            {
                ViewBag.Info = "Não foi possível se comunicar com a base de dados";
            }

            return View(dadosUsuario);
        }

        [Authorize]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult UpdateAccount(Cliente cliente)
        {
            var dadosUsuario = new Cliente();
            try
            {
                ViewBag.QtTelevisoes = new TelevisoesApplication(_context).GetAllByIdUser(idUsuario).Count;

                if (ModelState.IsValid)
                {
                    cliente.Id = idUsuario; 
                    var atualziarUsuario = new ClienteApplication(_context).Update(cliente);
                    ViewBag.InfoOk = atualziarUsuario;
                }
                else
                {
                    ViewBag.InfoOk = "Os dados estão inválidos";
                }
            }
            catch (Exception)
            {
                ViewBag.InfoOk = "Não foi possível se comunicar com a base de dados";
            }

            return View(dadosUsuario);
        }

        [Authorize]
        [Route("/Home/EditTelevision/{idTelevision}")]
        public IActionResult EditTelevision(int idTelevision = -1)
        {
            var listaDeVideos = new List<Videos>();
            try
            {
                //pega a lista de videos cadastrados dessa televisao que pertence a esse usuari
                listaDeVideos = new VideosApplication(_context).GetAllVideosByIdTelevisao(idTelevision, idUsuario);
                
                //pega as informaçoes da televisao para exibir no card
                var dadosTelevisao = new TelevisoesApplication(_context).GetById(idTelevision);
                ViewBag.NomeTelevisao = dadosTelevisao.Nome;
                ViewBag.CodeTV = dadosTelevisao.Codigo;

                //caso o usuario tente alterar uma televisao que não é dele ou não existe
                if (listaDeVideos == null)
                {
                    ViewBag.Info = "Televisão não encontrada, por favor, tente novamente";
                }
            }
            catch (Exception)
            {
                ViewBag.Info = "Não foi possível se comunicar com a base de dados";
            }

            return View(listaDeVideos);
        }

        [HttpDelete]
        [AllowAnonymous]
        public async Task<string> DeleteVideo(string codigoTelevision, int idVideo)
        {
            try
            {
                //pega os dados da televisao que contem o video
                var televisaoDeletar = new TelevisoesApplication(_context).GetByCode(codigoTelevision);
                televisaoDeletar.Modificado = 1;
                

                //se a televisao vier nulo, vao informar uma mensagem de erro
                if (televisaoDeletar != null)
                {
                    //senao vai verificar se a televisao pertence ao usuario
                    if (televisaoDeletar.FkIdCliente.Equals(idUsuario))
                    {
                        //se a televisao pertencer ao usuario, ve se existe um video com esse id
                        var todosOsVideos = new VideosApplication(_context).GetAllVideosByIdTelevisao(televisaoDeletar.Id, idUsuario);
                        var videoDeletar = todosOsVideos.Where(x => x.Id.Equals(idVideo)).ToList().FirstOrDefault();

                        if (videoDeletar != null)
                        {
                            //pega o nome do arquivo atraves da url
                            var nomeArquivo = new Directories().GetFileNameByLink(videoDeletar.Url);

                            //deleta o arquivo pelo nome
                            var deletarArquivo = new Manipulation(_provedorDiretoriosArquivos).DeletarArquivo(_provedorDiretoriosArquivos.GetFileInfo("/wwwroot/Videos/" + nomeArquivo).PhysicalPath);

                            //deleta o vídeo do banco de dados
                            var deletarVideo = new VideosApplication(_context).Delete(videoDeletar);

                            //define que a televisão foi modificada
                            var atulizarTelevisao = new TelevisoesApplication(_context).Update(televisaoDeletar);

                            return deletarVideo;
                        }
                        else
                        {
                            return "Você não tem permissão para fazer qualquer modificação nesse vídeo.";
                        }
                    }
                    else
                    {
                        return "Você não tem permissão para fazer qualquer modificação nessa televisão.";
                    }
                }
                else
                {
                    return "Televisão não encontrada. Por favor tente novamente";
                }
            }
            catch (Exception)
            {
                return "Ocorreu algum erro ao se comunicar com a base de dados";
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        public async Task<string> DeleteTelevision(string codigoTelevision)
        {
            try
            {
                //pega os dados da televisao que será deletada
                var televisaoDeletar = new TelevisoesApplication(_context).GetByCode(codigoTelevision);

                //se a televisao vier nulo, vao informar uma mensagem de erro
                if (televisaoDeletar != null)
                {
                    //senao vai verificar se a televisao pertence ao usuario
                    if (televisaoDeletar.FkIdCliente.Equals(idUsuario))
                    {
                        var todosOsVideos = new VideosApplication(_context).GetAllVideosByIdTelevisao(televisaoDeletar.Id, idUsuario);

                        for (int i = 0; i < todosOsVideos.Count; i++)
                        {
                            //pega o nome do arquivo através do link
                            var nomeArquivo = new Directories().GetFileNameByLink(todosOsVideos[i].Url);
                            
                            //deleta o arquivo atraves do link
                            var deletarArquivo = new Manipulation(_provedorDiretoriosArquivos).DeletarArquivo(_provedorDiretoriosArquivos.GetFileInfo("/wwwroot/Videos/" + nomeArquivo).PhysicalPath);

                            //deleta o vídeo do banco
                            var deletarVideo = new VideosApplication(_context).Delete(todosOsVideos[i]);
                        }

                        //caso pertença ao usuario deleta a televisao e os videos dela
                        var mensagemDeletado = new TelevisoesApplication(_context).Delete(televisaoDeletar);

                        if (mensagemDeletado.Equals("Televisão deletada com sucesso"))
                        {
                            return "Televisão deletada com sucesso!";
                        }
                        else
                        {
                            return mensagemDeletado;
                        }
                    }
                    else
                    {
                        return "Você não tem permissão para fazer qualquer modificação nessa televisão.";
                    }
                }
                else
                {
                    return "Televisão não encontrada. Por favor tente novamente";
                }
            }
            catch (Exception)
            {
                return "Ocorreu algum erro ao se comunicar com a base de dados";
            }
        }

        [Authorize]
        [Route("/Home/EditTelevision/{idTelevision}")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult EditTelevision(Televisoes televisoes, int idTelevision = -1)
        {
            try
            {
                //pega os dados da televisao por id
                var televisao = new TelevisoesApplication(_context).GetById(idTelevision);

                if (televisao != null)
                {
                    //altera o nome e o estado da televisao e atualiza no banco
                    televisao.Nome = televisoes.Nome;
                    televisao.Modificado = 1;
                    var atualizarTelevisao = new TelevisoesApplication(_context).Update(televisao);

                    if (televisao.Nome!= televisoes.Nome)
                    {
                        ViewBag.Info = atualizarTelevisao;
                    }

                    if (HttpContext.Request.Form.Files != null)
                    {
                        if (HttpContext.Request.Form.Files.Count > 0)
                        {
                            //pega todos os arquivos enviados via POST
                            var arquivos = HttpContext.Request.Form.Files;

                            //salva os arquivos na pasta wwwroot/Videos
                            var listaDeVideos = new Manipulation(_provedorDiretoriosArquivos).SalvarArquivos(arquivos);

                            //insere os dados dos vídeos no banco de dados
                            var videoAdicionar = new Videos
                            {
                                FkIdTelevisao = televisao.Id,
                                Url = DefaultRoute.Route + listaDeVideos[0]
                            };

                            var inseridoSucesso = new VideosApplication(_context).InsertConfirm(videoAdicionar);

                            if (!inseridoSucesso)
                            {
                                ViewBag.Info += " \nO vídeo não pode ser inserido. Tente novamente.";
                            }
                            else
                            {
                                ViewBag.Info += " \nTelevisão alterada com sucesso!";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Info = "Ocorreu algum erro ao tentar alterar a televisão";
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Info = "Ocorreu algum erro ao se comunicar com a base de dados";
            }

            return View(null);
        }

        [Authorize]
        public IActionResult AddTelevision()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddTelevision(Televisoes televisao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //verifica se os arquivos mandados nao sao nulos
                    if (HttpContext.Request.Form.Files != null)
                    {
                        if (HttpContext.Request.Form.Files.Count > 0)
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

                            //define que a televisao foi alterado por causa da inserção dos videos
                            televisao.Modificado = 1;

                            //adiciona a televisão
                            var adicionarTV = new TelevisoesApplication(_context).Insert(televisao);

                            //pega todos os arquivos enviados via POST
                            var arquivos = HttpContext.Request.Form.Files;

                            //salva os arquivos na pasta wwwroot/Videos
                            var listaDeVideos = new Manipulation(_provedorDiretoriosArquivos).SalvarArquivos(arquivos);

                            //insere os dados dos 4 vídeos no banco de dados

                            for (int i = 0; i < listaDeVideos.Count; i++)
                            {
                                var videoAdicionar = new Videos
                                {
                                    FkIdTelevisao = new TelevisoesApplication(_context).GetByCode(codigoGerado).Id,
                                    Url = DefaultRoute.Route + listaDeVideos[i]
                                };

                                var inseridoSucesso = new VideosApplication(_context).InsertConfirm(videoAdicionar);

                                if (!inseridoSucesso)
                                {
                                    ViewBag.Info += "O vídeo de número " + i + 1 + " não pode ser inserido. Tente novamente. \n";
                                }
                            }

                            ViewBag.Info += "Televisão e vídeos inseridos com sucesso";
                        }
                        else
                        {
                            ViewBag.Info = "Você deve inserir pelo menos um vídeo ao criar uma televisão";
                        }
                    }
                    else
                    {
                        ViewBag.Info = "Arquivos inválidos, por favor tente novamente";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Info = ex.ToString();
            }

            return View();
        }
    }
}
