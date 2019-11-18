using StreamAPI.Interfaces;
using StreamAPI.Models;
using System;
using System.Linq;

namespace StreamAPI.Applications
{
    public class TelevisionApplication : BaseApplication<Televisoes>, ITelevisionRepository
    {
        private SenaitvsContext _context;

        public TelevisionApplication(SenaitvsContext context)
        {
            _context = context;
        }

        public Televisoes GetByCode(string code)
        {
            try
            {
                var televisao = _context.Televisoes.Where(x => x.Codigo.Equals(code)).ToList();

                return televisao.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
