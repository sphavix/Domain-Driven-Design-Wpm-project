using Wpm.Clinic.Api.Application.Commands;
using Wpm.Clinic.Api.Infrastructure;
using Wpm.Clinic.Domain.Entities;

namespace Wpm.Clinic.Api.Application.Services
{
    public class ClinicApplicationService(ClinicDbContext context)
    {
        public async Task<Guid> Handle(StartConsultationCommand command)
        {
            var consultation = new Consultation(command.PatientId);
            await context.Consultations.AddAsync(consultation);
            await context.SaveChangesAsync();
            return consultation.Id;
        }


        public async Task Handle(EndConsultationCommand command)
        {
            var consultation = await context.Consultations.FindAsync(command.ConsultationId);
            if (consultation is null)
            {
                throw new InvalidOperationException($"Consultation with ID {command.ConsultationId} not found.");
            }
            consultation.End();
            await context.SaveChangesAsync();
        }

        public async Task Handle(SetDiagnosisCommand command)
        {
            var consultation = await context.Consultations.FindAsync(command.ConsultationId);
            if (consultation is null)
            {
                throw new InvalidOperationException($"Consultation with ID {command.ConsultationId} not found.");
            }
            consultation.SetDiagnosis(command.Diagnosis);
            await context.SaveChangesAsync();
        }

        public async Task Handle(SetTreatmentCommand command)
        {
            var consultation = await context.Consultations.FindAsync(command.ConsultationId);
            if (consultation is null)
            {
                throw new InvalidOperationException($"Consultation with ID {command.ConsultationId} not found.");
            }
            consultation.SetTreatment(command.Treatment);
            await context.SaveChangesAsync();
        }

        public async Task Handle(SetWeightCommand command)
        {
           var consultation = await context.Consultations.FindAsync(command.ConsultationId);
           if (consultation is null)
           {
               throw new InvalidOperationException($"Consultation with ID {command.ConsultationId} not found.");
           }
           consultation.SetWeight(command.Weight);
           await context.SaveChangesAsync();
        }

        public async Task Handle(AdministerDrugCommand command)
        {
            var consultation = await context.Consultations.FindAsync(command.ConsultationId);
            if (consultation is null)
            {
                throw new InvalidOperationException($"Consultation with ID {command.ConsultationId} not found.");
            }
            consultation.AdministerDrug(command.DrugId, new Domain.ValueObjects.Dose(command.Quantity, command.UnitOfMeasure));
            await context.SaveChangesAsync();
        }

        public async Task Handle(RegisterVitalSignsCommand command)
        {
            var consultation = await context.Consultations.FindAsync(command.ConsultationId);
            if (consultation is null)
            {
                throw new InvalidOperationException($"Consultation with ID {command.ConsultationId} not found.");
            }
            consultation.RegisterVitalSigns(command.VitalSignsReadings
                                                   .Select(r => new VitalSigns(r.ReadingDateTime, 
                                                                               r.Temperature, 
                                                                               r.HeartRate, 
                                                                               r.RespiratoryRate)));
            await context.SaveChangesAsync();
        }
    }
}
