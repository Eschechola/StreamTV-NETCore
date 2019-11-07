using StreamTV.Models;

namespace StreamTV.Interfaces
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Cliente GetByLogin(Cliente cliente);
        Cliente GetByEmail(string email);
    }
}
