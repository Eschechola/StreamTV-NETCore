using System;
using System.Linq;
using StreamTV.Context;
using StreamTV.Interfaces;
using StreamTV.Models;

namespace StreamTV.Application
{
    public class ClienteApplication : BaseApplication<Cliente>, IClienteRepository
    {
        private DatabaseContext _context;
        public ClienteApplication(DatabaseContext context)
        {
            _context = context;
        }

        public Cliente GetByLogin(Cliente cliente)
        {
            try
            {
                var usuario = _context.Cliente.Where(
                    x => 
                    x.Email.ToLower().Equals(cliente.Email.ToLower()) &&
                    x.Senha.Equals(cliente.Senha)
                );

                return usuario.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override Cliente GetById(int id)
        {
            try
            {
                var usuario = _context.Cliente.Where(x => x.Id.Equals(id));

                return usuario.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override string Update(Cliente cliente)
        {
            try
            {
                _context.Cliente.Update(cliente);
                _context.SaveChanges();

                return "Cliente atualizado com sucesso!";
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public Cliente GetByEmail(string email)
        {
            try
            {
                var usuario = _context.Cliente.Where(x => x.Email.ToLower().Equals(email.ToLower()));

                return usuario.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
