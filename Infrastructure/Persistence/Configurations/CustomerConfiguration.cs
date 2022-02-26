using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{

    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.Property(t => t.Firstname)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(t => t.Lastname)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(t => t.PhoneNumber)
                .HasMaxLength(32)
                .IsRequired();
            builder.Property(t => t.Email)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(t => t.BankAccountNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(b => new {b.Firstname, b.Lastname, b.DateOfBirth})
                .IsUnique();
            builder.HasIndex(b => b.Email)
                .IsUnique();

            //builder
            //    .OwnsOne(b => b.Colour);


            //builder.Ignore(e => e.DomainEvents);

            //builder.Property(t => t.Title)
            //    .HasMaxLength(200)
            //    .IsRequired();

        }
    }
}