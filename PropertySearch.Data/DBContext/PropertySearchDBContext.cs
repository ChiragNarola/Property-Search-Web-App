using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PropertySearch.Data.Models;

namespace PropertySearch.Data.DBContext
{
    public class PropertySearchDBContext : IdentityDbContext
    {
        public PropertySearchDBContext(
            DbContextOptions<PropertySearchDBContext> options
            )
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        #region DbSets

        public virtual DbSet<Property> Properties { get; set; } = null!;
        public virtual DbSet<Space> Spaces { get; set; } = null!;


        #endregion
       
        #region ModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Get all entity types in the model
            var entityTypes = modelBuilder.Model.GetEntityTypes();

            // Loop through each entity type
            foreach (var entityType in entityTypes)
            {
                // Loop through all the foreign keys for each entity type
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    // Set the delete behavior to Restrict
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType.IsEnum)
                    {
                        var type = typeof(EnumToStringConverter<>).MakeGenericType(property.ClrType);
                        var converter = Activator.CreateInstance(type, new ConverterMappingHints()) as ValueConverter;

                        property.SetValueConverter(converter);
                    }
                    else if (Nullable.GetUnderlyingType(property.ClrType)?.IsEnum == true)
                    {
                        var type = typeof(EnumToStringConverter<>).MakeGenericType(Nullable.GetUnderlyingType(property.ClrType)!);
                        var converter = Activator.CreateInstance(type, new ConverterMappingHints()) as ValueConverter;

                        property.SetValueConverter(converter);
                    }
                }
            }
            // Property indexes
            modelBuilder.Entity<Property>()
                .HasIndex(p => p.Price)
                .HasDatabaseName("IX_Property_Price");

            modelBuilder.Entity<Property>()
                .HasIndex(p => p.Type)
                .HasDatabaseName("IX_Property_Type");

            // Space indexes
            modelBuilder.Entity<Space>()
                .HasIndex(s => s.Type)
                .HasDatabaseName("IX_Space_Type");

            modelBuilder.Entity<Space>()
                .HasIndex(s => s.Size)
                .HasDatabaseName("IX_Space_Size");

        }
        #endregion

        public int Save() => SaveChanges();
        public Task<int> SaveAsync() => SaveChangesAsync();
    }

}
