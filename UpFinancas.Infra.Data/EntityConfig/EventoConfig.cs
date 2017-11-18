using System.Data.Entity.ModelConfiguration;
using UpFinancas.Domain.Entities;

namespace UpFinancas.Infra.Data.EntityConfig
{
    //http://www.codeproject.com/Articles/796540/Relationship-in-Entity-Framework-Using-Code-First
    class EventoConfig :EntityTypeConfiguration<Evento>
    {
        public EventoConfig()
        {
            ToTable("eventos");

            HasKey(u => u.Id);

            Property(u => u.Id).HasColumnName("id");

            Property(u => u.Descricao).HasMaxLength(150).IsRequired().HasColumnName("descricao");

            Property(u => u.Tipo).IsRequired().HasColumnName("tipo");

            Property(u => u.Valor).IsRequired().HasColumnName("valor");

            Property(u => u.ValorJuros).HasColumnName("valor_juros");

            Property(u => u.ValorDesconto).HasColumnName("valor_desconto");

            Property(u => u.Observacao).HasMaxLength(300).HasColumnName("observacao");

            Property(u => u.DataLancamento).IsRequired().HasColumnName("data_lancamento");

            Property(u => u.DataVencimento).HasColumnName("data_vencimento");

            Property(u => u.DataPagamento).HasColumnName("data_pagamento");

            Property(u => u.ContaId).IsRequired().HasColumnName("conta_id");

            Property(u => u.PessoaId).HasColumnName("pessoa_id");

            Property(u => u.CategoriaId).HasColumnName("categoria_id");

            Property(u => u.FormaId).IsRequired().HasColumnName("forma_id");

            HasRequired(u => u.Conta).WithMany(c => c.Eventos).HasForeignKey(t => t.ContaId).WillCascadeOnDelete(true);

            HasOptional(u => u.Pessoa).WithMany(c => c.Eventos).HasForeignKey(t => t.PessoaId).WillCascadeOnDelete(true);

            HasOptional(u => u.Categoria).WithMany(c => c.Eventos).HasForeignKey(t => t.CategoriaId).WillCascadeOnDelete(true);

            HasRequired(u => u.Forma).WithMany(c => c.Eventos).HasForeignKey(t => t.FormaId).WillCascadeOnDelete(true);
        }
    }
}
