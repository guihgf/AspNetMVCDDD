using System.Data.Entity.ModelConfiguration;
using UpFinancas.Domain.Entities;

namespace UpFinancas.Infra.Data.EntityConfig
{
    class PessoaConfig :EntityTypeConfiguration<Pessoa>
    {
        public PessoaConfig()
        {
            ToTable("pessoas");

            HasKey(u => u.Id);

            Property(u => u.Id).HasColumnName("id");

            Property(u => u.Nome).HasMaxLength(150).IsRequired().HasColumnName("nome");

            Property(u => u.Telefone).HasMaxLength(30).HasColumnName("telefone");

            Property(u => u.Celular).HasMaxLength(30).HasColumnName("celular");

            Property(u => u.Email).HasMaxLength(150).HasColumnName("email");

            Property(u => u.Rua).HasMaxLength(150).HasColumnName("rua");

            Property(u => u.Numero).HasMaxLength(20).HasColumnName("numero");

            Property(u => u.Bairro).HasMaxLength(150).HasColumnName("bairro");

            Property(u => u.Cidade).HasMaxLength(150).HasColumnName("cidade");

            Property(u => u.Complemento).HasMaxLength(200).HasColumnName("complemento");

            Property(u => u.DataCadastro).IsRequired().HasColumnName("data_cadastro");

            Property(u => u.DataDesativacao).HasColumnName("data_desativacao");

            Property(u => u.UsuarioId).IsRequired().HasColumnName("usuario_id");

            HasRequired(u => u.Usuario).WithMany(c => c.Pessoas).HasForeignKey(t => t.UsuarioId).WillCascadeOnDelete(true);

        }
    }
}
