using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbySalto.Junior.Infrastructure.Database.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.CustomerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.DeliveryAddress)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(o => o.ContactNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(o => o.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("EUR");

            builder.Property(o => o.Note)
                .HasMaxLength(500);

            builder.HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);
        }
    }
}