using System;
using System.Collections.Generic;

namespace StreamAPI.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Televisoes = new HashSet<Televisoes>();
        }

        public int Id { get; set; }
        public string Instituicao { get; set; }
        public string Responsavel { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Televisoes> Televisoes { get; set; }
    }
}
