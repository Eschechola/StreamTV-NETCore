using StreamAPI.Models;
using System.Collections.Generic;

namespace StreamAPI.Interfaces
{
    public interface IVideosRepository : IBaseRepository<Videos>
    {
        public List<Videos> GetAllByCode(string code);
    }
}
