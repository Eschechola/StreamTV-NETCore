using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StreamTV.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Televisoes = new HashSet<Televisoes>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da instituição deve ser inserido")]
        [MinLength(3, ErrorMessage = "O nome da instituição deve ter no mínimo 3 caracteres")]
        [MaxLength(200, ErrorMessage = "O nome da instituição é muito grande, use até 200 caracteres")]
        public string Instituicao { get; set; }

        [Required(ErrorMessage = "O nome do responsável deve ser inserido")]
        [MinLength(3, ErrorMessage = "O nome do responsável deve ter no mínimo 3 caracteres")]
        [MaxLength(200, ErrorMessage = "O nome do responsável é muito grande, use até 200 caracteres")]
        public string Responsavel { get; set; }

        [Required(ErrorMessage = "A senha deve ser inserida.")]
        [MinLength(3, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        [MaxLength(200, ErrorMessage = "A senha é muito grande, use até 100 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O telefone deve ser inserirdo.")]
        [MinLength(8, ErrorMessage ="O telefone está inválido. Por favor tente outro")]
        [MaxLength(20, ErrorMessage = "O telefone está inválido Por favor tente outro")]
        public string Telefone { get; set; }

        public string Endereco { get; set; }

        [Required(ErrorMessage = "O email deve ser inserido")]
        [MinLength(10, ErrorMessage = "O email deve ter no mínimo 10 caracteres")]
        [MaxLength(300, ErrorMessage = "O email deve ter no máximo 300 caracteres")]
        [DataType(DataType.EmailAddress, ErrorMessage = "O email está inválido.")]
        public string Email { get; set; }

        public virtual ICollection<Televisoes> Televisoes { get; set; }
    }
}
