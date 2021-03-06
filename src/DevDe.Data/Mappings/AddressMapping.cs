using AppMvcBasic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevDe.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(a => a.Number)
                .IsRequired()
                .HasColumnType("varchar(6)");

            builder.Property(a => a.Complement)
                .HasColumnType("varchar(50)");

            builder.Property(a => a.Neighborhood)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(a => a.City)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(a => a.ZipCode)
                .IsRequired()
                .HasColumnType("varchar(8)")
                .HasMaxLength(8);

            builder.Property(a => a.State)
                .IsRequired()
                .HasColumnType("varchar(2)")
                .HasMaxLength(2);

            builder.ToTable("Addresses");

        }
    }
}
