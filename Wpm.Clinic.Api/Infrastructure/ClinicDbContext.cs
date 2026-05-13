using Microsoft.EntityFrameworkCore;
using Wpm.Clinic.Domain.Entities;

namespace Wpm.Clinic.Api.Infrastructure
{
    public class ClinicDbContext(DbContextOptions<ClinicDbContext> options) : DbContext(options)
    {
        public DbSet<Consultation> Consultations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Consultation>(consultation =>
            {
                consultation.HasKey(c => c.Id);

                consultation.Property(p => p.PatientId)
                            .HasConversion(v => v.Value, v => new Domain.ValueObjects.PatientId(v));

                consultation.OwnsOne(p => p.Diagnosis);
                consultation.OwnsOne(p => p.Treatment);
                consultation.OwnsOne(p => p.CurrentWeight);

                consultation.OwnsMany(c => c.AdminsteredDrug, a =>
                {
                    a.WithOwner().HasForeignKey("ConsultationId");
                    a.OwnsOne(d => d.DrugId);
                    a.OwnsOne(d => d.Dose);
                });

                consultation.OwnsMany(c => c.VitalSignsReading, o =>
                {
                    o.WithOwner().HasForeignKey("ConsultationId");
                });
            });
        }
    }

    public static class ClinicDbContextExtensions
    {
        public static void EnsureDbIsCreated(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ClinicDbContext>();
            context.Database.EnsureCreated();
            context.Database.CloseConnection();
        }
    }
}
