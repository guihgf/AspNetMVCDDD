using System.Data.Entity.ModelConfiguration;
using UpFinancas.Domain.Entities;

namespace UpFinancas.Infra.Data.EntityConfig
{
    class FormaConfig :EntityTypeConfiguration<Forma>
    {
        public FormaConfig()
        {
            ToTable("formas");

            HasKey(u => u.Id);

            Property(u => u.Id).HasColumnName("id");

            Property(u => u.Nome).HasMaxLength(150).IsRequired().HasColumnName("nome");

            Property(u => u.UsuarioId).IsRequired().HasColumnName("usuario_id");

            Property(u => u.DataCadastro).IsRequired().HasColumnName("data_cadastro");

            Property(u => u.DataDesativacao).HasColumnName("data_desativacao");

            HasRequired(u => u.Usuario).WithMany(c => c.Formas).HasForeignKey(t => t.UsuarioId).WillCascadeOnDelete(false);
        }
    }
}
