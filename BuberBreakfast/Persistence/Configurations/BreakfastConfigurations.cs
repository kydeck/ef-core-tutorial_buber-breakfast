using BuberBreakfast.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuberBreakfast.Persistence.Configurations
{
    public class BreakfastConfigurations : IEntityTypeConfiguration<Breakfast>
    {
        // Define the configuration for EF Core to convert code models to DB schema. By default EFCore does not know to look for this file, so explicitly define it in {name}DbContext.cs - override the "OnModelCreating()" method and utilize ".ApplyConfigurationsFromAssembly()" method.
        public void Configure(EntityTypeBuilder<Breakfast> builder)
        {
            // Check if Id is being created, or if Database needs to determine the Id. In this project, the constructor will create the Id property
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .HasMaxLength(Breakfast.MaxNameLength);

            builder.Property(b => b.Description)
                .HasMaxLength(Breakfast.MaxDescriptionLength);

            // Convert complex object to string in DB
            builder.Property(b => b.Savory)
                .HasConversion(
                    v => string.Join(',', v),
                     v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            // Convert complex object to string in DB
            builder.Property(b => b.Sweet)
                .HasConversion(
                    v => string.Join(',', v),
                     v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        }
    }
}
