using StreamTV.Models;

namespace StreamTV.Interfaces
{
    public interface IVideosRepository : IBaseRepository<Videos>
    {
        public bool InsertConfirm(Videos video);
    }
}
