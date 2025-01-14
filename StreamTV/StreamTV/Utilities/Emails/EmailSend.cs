﻿using StreamTV.Models.ModelsEmail;
using System;
using System.Net;
using System.Net.Mail;

namespace StreamTV.Utilities.Emails
{
    public class EmailSend
    {
        public bool SendEmail(Email email)
        {
            try
            {
                //cria a mensagem de email
                MailMessage message = new MailMessage();
                //instancia o cliente smtp
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                //email que vai enviar (no caso nós)
                message.From = new MailAddress("suporte.streamtvs@gmail.com");
                //para quem vai enviar (no caso o usuario)
                message.To.Add(new MailAddress(email.EmailCliente));
                //atribui assundo do email recebido como parametro na classe email
                message.Subject = email.Assunto;
                //habilita usar html na mensagem
                message.IsBodyHtml = true;
                //atribui a mensagem recebida como parametro na classe email
                message.Body = email.Conteudo;
                //porta que usarei pra enviar o email
                smtp.Port = 587;
                //smtp.Port = 25;
                //habilita camada de segurança
                smtp.EnableSsl = true;
                //desabilita as credenciais padrao
                smtp.UseDefaultCredentials = false;
                //da as credenciais do nosso para poder acessar e enviar
                smtp.Credentials = new NetworkCredential("suporte.streamtvs@gmail.com", "2345345234");
                //define o método de envio, no caso web
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //envia a mensagem
                smtp.Send(message);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
