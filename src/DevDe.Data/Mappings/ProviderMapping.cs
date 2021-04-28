using AppMvcBasic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevDe.Data.Mappings
{
    public class ProviderMapping : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Document)
                .IsRequired()
                .HasColumnType("varchar(14)");

            // EF Relation One to One 1 : 1
            builder.HasOne(p => p.Address)
                .WithOne(a => a.Provider);

            // EF Relation One to Many 1 : N
            builder.HasMany(p => p.Products)
                .WithOne(pr => pr.Provider)
                .HasForeignKey(pr => pr.ProviderId);


            builder.ToTable("Providers");
                
        }
    }
}
