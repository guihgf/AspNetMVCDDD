using System.Data.Entity.ModelConfiguration;
using UpFinancas.Domain.Entities;

namespace UpFinancas.Infra.Data.EntityConfig
{
    //http://www.codeproject.com/Articles/796540/Relationship-in-Entity-Framework-Using-Code-First
    class ContaConfig :EntityTypeConfiguration<Conta>
    {
        public ContaConfig()
        {
            ToTable("contas");

            HasKey(u => u.Id);

            Property(u => u.Id).HasColumnName("id");

            Property(u => u.Nome).HasMaxLength(150).IsRequired().HasColumnName("nome");

            Ignore(u => u.Saldo);

            Property(u => u.DataCadastro).IsRequired().HasColumnName("data_cadastro");

            Property(u => u.DataDesativacao).HasColumnName("data_desativacao");

            Property(u => u.UsuarioId).HasColumnName("usuario_id");

            Property(u => u.Padrao).IsRequired().HasColumnName("padrao");

            HasRequired(u => u.Usuario).WithMany(c => c.Contas).HasForeignKey(t => t.UsuarioId).WillCascadeOnDelete(true);

            
        }
    }
}
