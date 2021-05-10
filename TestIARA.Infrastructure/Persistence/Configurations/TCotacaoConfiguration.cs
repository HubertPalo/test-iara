using System;
using System.Collections.Generic;
using System.Text;
using TestIARA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestIARA.Infrastructure.Persistence.Configurations
{
    public class TCotacaoConfiguration : IEntityTypeConfiguration<TCotacao>
    {
        public void Configure(EntityTypeBuilder<TCotacao> builder)
        {
            // Key

            builder.HasKey(k => k.NumeroCotacao).HasName("PK_TCotacao");
            
            // Properties

            builder.Property(p => p.CNPJComprador).HasMaxLength(14).IsRequired();
            builder.Property(p => p.CNPJFornecedor).HasMaxLength(14).IsRequired();
            builder.Property(p => p.DataCotacao).HasMaxLength(8).IsRequired();
            builder.Property(p => p.DataEntregaCotacao).HasMaxLength(8).IsRequired();
            builder.Property(p => p.CEP).HasMaxLength(8).IsRequired();
            builder.Property(p => p.Logradouro).IsRequired(false);
            builder.Property(p => p.Complemento).IsRequired(false);
            builder.Property(p => p.Bairro).IsRequired(false);
            builder.Property(p => p.UF).HasMaxLength(2).IsRequired(false);
            builder.Property(p => p.Observacao).IsRequired(false);

            // Foreign Keys

        }
    }
}
