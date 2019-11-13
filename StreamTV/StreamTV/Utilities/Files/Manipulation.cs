using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StreamTV.Utilities.Files
{
    public class Manipulation
    {
        //variavel para poder acessar a pasta wwwroot nas funçoes que necessitam de imagem
        //dependencia será injetada na classe ClienteMessages
        private PhysicalFileProvider _provedorDiretoriosArquivos;
        private string diretorioRaiz = "wwwroot/Videos";

        public Manipulation(PhysicalFileProvider provedorDiretoriosArquivos)
        {
            _provedorDiretoriosArquivos = provedorDiretoriosArquivos;
        }

        public List<string> SalvarArquivos(IFormFileCollection arquivos)
        {
            List<string> listaDiretorioVideos = new List<string>();

            try
            {
                for(int i = 0; i < arquivos.Count; i++)
                {
                    if (arquivos[i].Length > 0)
                    {
                        //define o nome do arquivo como a hora atual
                        var nomeArquivo = DateTime.Now.ToString();

                        //retira os caracteres especiais
                        nomeArquivo = nomeArquivo.Replace("/", "_");
                        nomeArquivo = nomeArquivo.Replace(":", "_");
                        nomeArquivo = nomeArquivo.Replace(" ", "_");

                        // concatena nomeArquivo + extensão
                        nomeArquivo = "streamTV_Video_" + nomeArquivo + ".mp4";

                        // combina o diretorio do arquivo + diretorio
                        var diretorioDeArmazenamento = Path.Combine(_provedorDiretoriosArquivos.GetFileInfo(diretorioRaiz).PhysicalPath, nomeArquivo);

                        //copia a foto enviada e cola no diretório de armazenamento informado
                        using (FileStream streamDeDados = File.Create(diretorioDeArmazenamento))
                        {
                            arquivos[i].CopyTo(streamDeDados);
                            //streamDeDados.Flush();
                        }

                        listaDiretorioVideos.Add(nomeArquivo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaDiretorioVideos;
        }
    }
}
