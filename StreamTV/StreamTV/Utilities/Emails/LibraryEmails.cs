using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamTV.Utilities.Emails
{
    public class LibraryEmails
    {
        public static IDictionary<string, string> Messages = new Dictionary<string, string>
        {
            {"Password Recovery", "<style> *{ padding: 0; margin:0; text-align: justify; font-family: arial; width: 90%; } </style> <div style=\"width: 100vw; height: 100vh; background-color: #03266A;padding-left: 5%;\">  \n \n <h2 style=\"color: #fafafa; text-align: center;\">Recuperação de senha...</h2>  \n \n <p style=\"color: #fafafa\"> Foi solicidado a opção \"esqueci senha\" e foi informado o seu e-mail, caso não tenha sido você por favor desconsidere essa mensagem.  \n \n Caso tenha sido você, não se preocupe, aqui está sua senha: <strong>{0}</strong>  \n \n Guarde - a em algum lugar seguro, ou altere - a no nosso Entre.  \n \n <strong>Atenciosamente, Equipe StreamTV :)</strong> </p> </div>"},
            {"", ""}
        };
    }
}
