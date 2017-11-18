Projeto com a estrutura parecida do PHPDDD, porém mais completo, poderoso e simples devidos às libs e .NET Framework.
Aqui consegui implementar ViewModel com o AutoMapper e o relacionamento entre Model e Table ficou simples graças ao Entity Framework utilizado também para Migrations.
Todo projeto utiliza IOC graças ao Unity for IOC.

Para executar o projeto basta baixar as dependências no nugget e executar as migrations. É necessário que você configure a sua base de dados no Web.config, para envio de e-mails é necessário cadastrar os dados do seu servidor de e-mail na classe MailServie em UpFinancas.Infra.Data\Services.
