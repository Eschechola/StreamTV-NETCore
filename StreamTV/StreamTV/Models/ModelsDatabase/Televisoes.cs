using System;
using System.Collections.Generic;

namespace StreamTV.Models
{
    public partial class Televisoes
    {
        public Televisoes()
        {
            Videos = new HashSet<Videos>();
        }

        public int Id { get; set; }
        public int? FkIdCliente { get; set; }
        public string Nome { get; set; }

        public virtual Cliente FkIdClienteNavigation { get; set; }
        public virtual ICollection<Videos> Videos { get; set; }
    }
}
