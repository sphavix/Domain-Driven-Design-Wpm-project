using Microsoft.EntityFrameworkCore;
using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Api.Infrastructure
{
    public class ManagementDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pet>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BreedId).HasConversion(v => v.Value, v => BreedId.Create(v));
                entity.OwnsOne(x => x.Weight);
            });
        }
    }

    public static class ManagementDbContextExtensions
    {
        public static void EnsureDbIsCreated(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ManagementDbContext>();
            context.Database.EnsureCreated();
            context.Database.CloseConnection();

        }
    }
}
