using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using UpFinancas.Domain.Entities;

namespace UpFinancas.Infra.Data.EntityConfig
{
    class UsuarioConfig:EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            ToTable("usuarios");

            HasKey(u => u.Id);

            Property(u => u.Id).HasColumnName("id");

            Property(u => u.Nome).HasMaxLength(150).IsRequired().HasColumnName("nome");

            Property(u => u.Email).HasMaxLength(150).IsRequired().HasColumnName("email").HasColumnAnnotation(
                                                                                                            IndexAnnotation.AnnotationName,
                                                                                                            new IndexAnnotation(
                                                                                                                new IndexAttribute("IX_EMAIL_USUARIO", 1) { IsUnique = true })); ;

            Property(u => u.Senha).HasMaxLength(300).IsFixedLength().HasColumnName("senha");

            Property(u => u.DataCadastro).IsRequired().HasColumnName("data_cadastro");

            Property(u => u.DataCancelamento).HasColumnName("data_cancelamento");

            Property(u => u.DataReativacao).HasColumnName("data_reativacao");

            Property(u => u.DataConfirmacao).HasColumnName("data_confirmacao");
        }
    }
}
