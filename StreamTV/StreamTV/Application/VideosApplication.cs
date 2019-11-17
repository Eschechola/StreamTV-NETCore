using StreamTV.Context;
using StreamTV.Interfaces;
using StreamTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StreamTV.Application
{
    public class VideosApplication : BaseApplication<Videos>, IVideosRepository
    {
        private DatabaseContext _context;
        public VideosApplication(DatabaseContext context)
        {
            _context = context;
        }

        public List<Videos> GetAllVideosByIdTelevisao(int idTelevisao, int idCliente)
        {
            try
            {
                var televisaoUsuario = _context.Televisoes.Where(x => x.FkIdCliente.Equals(idCliente) && x.Id.Equals(idTelevisao))
                    .ToList()
                    .FirstOrDefault();

                if (televisaoUsuario != null)
                {
                    var videosTelevisao = _context.Videos.Where(x => x.FkIdTelevisao.Equals(televisaoUsuario.Id)).ToList();
                    return videosTelevisao;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool InsertConfirm(Videos video)
        {
            try
            {
                _context.Add(video);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override string Delete(Videos video)
        {
            try
            {
                _context.Videos.Remove(video);
                _context.SaveChanges();

                return "Vídeo deletado com sucesso!";
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados";
            }
        }
    }
}
