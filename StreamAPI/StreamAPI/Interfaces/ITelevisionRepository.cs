using StreamAPI.Models;
using System.Collections.Generic;

namespace StreamAPI.Interfaces
{
    public interface ITelevisionRepository : IBaseRepository<Televisoes>
    {
        public Televisoes GetByCode(string code);
    }
}
