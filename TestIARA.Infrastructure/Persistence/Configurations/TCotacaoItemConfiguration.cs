using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestIARA.Domain.Entities;

namespace TestIARA.Infrastructure.Persistence.Configurations
{
    public class TCotacaoItemConfiguration : IEntityTypeConfiguration<TCotacaoItem>
    {
        public void Configure(EntityTypeBuilder<TCotacaoItem> builder)
        {
            // Keys 

            builder.HasKey(k => k.NumeroCotacaoItem).HasName("PK_TCotacaoItem");

            // Properties

            builder.Property(p => p.Descricao).IsRequired();
            builder.Property(p => p.NumeroItem).IsRequired();
            builder.Property(p => p.Preco).HasDefaultValue(0).IsRequired(false);
            builder.Property(p => p.Quantidade).IsRequired();
            builder.Property(p => p.Marca).IsRequired(false);
            builder.Property(p => p.Unidade).IsRequired(false);


            // Foreign Keys
            
            builder.HasOne(o => o.Cotacao)
                .WithMany(m => m.CotacaoItems)
                .HasForeignKey(fk => fk.NumeroCotacao)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
