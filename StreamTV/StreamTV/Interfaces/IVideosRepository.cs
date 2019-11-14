using StreamTV.Models;
using System.Collections.Generic;

namespace StreamTV.Interfaces
{
    public interface IVideosRepository : IBaseRepository<Videos>
    {
        public bool InsertConfirm(Videos video);
        public List<Videos> GetAllVideosByIdTelevisao(int idTelevisao, int idCliente);
    }
}
