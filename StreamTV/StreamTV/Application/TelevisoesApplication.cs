using System;
using StreamTV.Context;
using StreamTV.Interfaces;
using StreamTV.Models;
using System.Linq;
using System.Collections.Generic;

namespace StreamTV.Application
{
    public class TelevisoesApplication : BaseApplication<Televisoes>, ITelevisoesRepository
    {
        private DatabaseContext _context;
        public TelevisoesApplication(DatabaseContext context)
        {
            _context = context;
        }

        public List<Televisoes> GetAllByIdUser(int idUsuario)
        {
            try
            {
                var listaDeTelevisoes = _context.Televisoes.Where(x => x.FkIdCliente.Equals(idUsuario));
                return listaDeTelevisoes.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Televisoes GetByCode(string code)
        {
            try
            {
                var televisao = _context.Televisoes.Where(x => x.Codigo.Equals(code)).ToArray().FirstOrDefault();
                return televisao;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public override string Insert(Televisoes televisao)
        {
            try
            {
                _context.Add(televisao);
                _context.SaveChanges();

                return "Inserido com sucesso";
            }
            catch (Exception)
            {
                return "Erro ao se comunicar com a base de dados";
            }
        }

        public override Televisoes GetById(int id)
        {
            try
            {
                var televisao = _context.Televisoes.Where(x => x.Id.Equals(id));
                return televisao.ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override string Update(Televisoes televisao)
        {
            try
            {
                _context.Update(televisao);
                _context.SaveChanges();

                return "Nome atualizado com sucesso";
            }
            catch (Exception)
            {
                return "Erro ao se comunicar com a base de dados";
            }
        }

        public override string Delete(Televisoes televisao)
        {
            try
            {
                _context.Remove(televisao);
                _context.SaveChanges();

                return "Televisão: "+televisao.Nome+", deletada com sucesso";
            }
            catch (Exception)
            {
                return "Erro ao se comunicar com a base de dados";
            }
        }

    }
}
