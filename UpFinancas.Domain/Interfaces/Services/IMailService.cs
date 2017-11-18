namespace UpFinancas.Domain.Interfaces.Services
{
    public interface IMailService
    {
        void Enviar (string destinatario,string assunto,string msg);
        void Dispose();
    }
}