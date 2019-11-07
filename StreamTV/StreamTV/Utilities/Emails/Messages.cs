using StreamTV.Models.ModelsEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamTV.Utilities.Emails
{
    public class Messages
    {
        private string _emailUsuario { get; set; }

        public Messages(string emailUsuario)
        {
            _emailUsuario = emailUsuario;
        }

        public bool ForgotPassword()
        {
            try
            {
                var emailTemplate = new Email
                {
                    EmailCliente = _emailUsuario,
                    Assunto = "Recuperação de senha",
                    Conteudo = LibraryEmails.Messages.Values.ToList()[0]
                };

                var sucesso = new EmailSend().SendEmail(emailTemplate);

                if (sucesso)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
