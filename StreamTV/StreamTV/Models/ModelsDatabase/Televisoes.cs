using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        
        [Required(ErrorMessage = "O nome da televisão deve ser inserido")]
        public string Nome { get; set; }
        public string Codigo { get; set; }

        public virtual Cliente FkIdClienteNavigation { get; set; }
        public virtual ICollection<Videos> Videos { get; set; }
    }
}
