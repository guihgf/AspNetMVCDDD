using UpFinancas.Domain.Interfaces.Services;

namespace UpFinancas.Domain.Entities
{
    public class MailTemplate
    {
        public string Assunto { get; private set; }
        public string CorpoEmail { get; set; }
        public string Email { get; private set; }
        public string Nome { get; private set; }

        public MailTemplate(string email,string nome)
        {
            Email = email;
            Nome = nome;
        }

        public void EmailCadastro(string senha)
        {
            Assunto = "Olá  " + Nome + "! Bem vindo(a) ao Up Finanças";
            CorpoEmail="<b>Bem vindo(a) ao Upfinanças.</b><br/><br/>"+
                            "Segue os dados para acesso: <br/><br/>"+
                            "Login para acesso: <b>"+Email+"</b><br/>"+
                            "Senha: <b>" +senha+ "</b><br/><br/>"+
                            "É recomendado que ao acessar o sistema você altere esta senha. <br/><br/>"+
                            "Aprenda a utilizar o sistema rapidinho em https://www.youtube.com/watch?v=LYHeZIHiFdk&list=PL9N6g2ub0edtlr53h15NvkmAcUpgngHay <br/><br/>"+
                            "Tenha uma vida financeira mais saudável com a Upfinanças. Controle contas, lançamentos, grupos e muito mais!";
        }

        public void EmailResetarSenha(string senha)
        {
            Assunto = "Olá  " + Nome + "! A sua senha foi alterada";
            CorpoEmail = Nome+ ", conforme solicitado a sua senha foi resetada, a nova senha é: <b>"+senha+"</b><br/><br/>" +
                            "Login para acesso: <b>" + Email + "</b><br/>" +
                            "Senha: <b>" + senha + "</b><br/><br/>" +
                            "É recomendado que ao acessar o sistema você altere esta senha. <br/><br/>" +
                            "Tenha uma vida financeira mais saudável com a Upfinanças. Controle contas, lançamentos, grupos e muito mais!";
        }
    }
}