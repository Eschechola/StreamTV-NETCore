using StreamTV.Models;
using System.Collections.Generic;

namespace StreamTV.Interfaces
{
    public interface ITelevisoesRepository : IBaseRepository<Televisoes>
    {
        public Televisoes GetByCode(string code);
        public List<Televisoes> GetAllByIdUser(int idUsuario);
    }
}
