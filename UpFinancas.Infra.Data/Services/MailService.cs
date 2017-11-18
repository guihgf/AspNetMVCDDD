using System.Net;
using System.Net.Mail;
using UpFinancas.Domain.Interfaces.Services;

namespace UpFinancas.Infra.Data.Services
{
    public class MailService:IMailService
    {
        private readonly MailMessage _message;
        private readonly SmtpClient _smtp;

        public MailService()
        {
            _message = new MailMessage();
            _smtp = new SmtpClient();
        }

        public void Enviar (string destinatario,string assunto,string msg)
        {
            const string remetente = "suporte@upfinancas.com.br";
            
            _message.Sender = new MailAddress(remetente);
            _message.From = new MailAddress(remetente);
            _message.To.Add(new MailAddress(destinatario));
            _message.Subject = assunto;
            _message.Body = msg;
            _message.IsBodyHtml = true;
            _message.Headers.Add("X-Organization", "Up Finanças");
            _smtp.Credentials = new NetworkCredential("seuusuario", "suasenha");
            _smtp.Host = "smtp.zoho.com";
            _smtp.Port = 587;
            _smtp.EnableSsl = true;
            _smtp.Send(_message);
            

        }
        public void Dispose()
        {
            _message.Dispose();
            _smtp.Dispose();
        }
    }
}
