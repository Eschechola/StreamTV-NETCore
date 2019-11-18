using StreamAPI.Interfaces;
using StreamAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StreamAPI.Applications
{
    public class VideosApplication : BaseApplication<Videos>, IVideosRepository
    {
        private SenaitvsContext _context;

        public VideosApplication(SenaitvsContext context)
        {
            _context = context;
        }

        public List<Videos> GetAllByCode(string code)
        {
            try
            {
                var televisao = _context.Televisoes.Where(x => x.Codigo.Equals(code)).ToList().FirstOrDefault();

                if (televisao != null)
                {
                    var listaDeVideos = _context.Videos.Where(x => x.FkIdTelevisao.Equals(televisao.Id));
                    
                    return listaDeVideos.ToList();
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
    }
}
