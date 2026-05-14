using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Wpm.Clinic.Api.Application.Commands;
using Wpm.Clinic.Api.Infrastructure;
using Wpm.Clinic.Domain.Entities;
using Wpm.SharedKernel.DomainEvents;

namespace Wpm.Clinic.Api.Application.Services
{
    public class ClinicApplicationService(ClinicDbContext context)
    {
        public async Task<Guid> Handle(StartConsultationCommand command)
        {
            var consultation = new Consultation(command.PatientId);
            await SaveAsync(consultation);
            return consultation.Id;
        }


        public async Task Handle(EndConsultationCommand command)
        {
            var consultation = await context.Consultations.FindAsync(command.ConsultationId);
            if (consultation is null)
            {
                throw new InvalidOperationException($"Consultation with ID {command.ConsultationId} not found.");
            }
            await context.SaveChangesAsync();
        }

        public async Task Handle(SetDiagnosisCommand command)
        {
            var consultation = await LoadAsync(command.ConsultationId);
            consultation.SetDiagnosis(command.Diagnosis);
            await SaveAsync(consultation);
        }

        public async Task Handle(SetTreatmentCommand command)
        {
            var consultation = await context.Consultations.FindAsync(command.ConsultationId);
            if (consultation is null)
            {
                throw new InvalidOperationException($"Consultation with ID {command.ConsultationId} not found.");
            }
            //consultation.SetTreatment(command.Treatment);
            await context.SaveChangesAsync();
        }

        public async Task Handle(SetWeightCommand command)
        {
           var consultation = await context.Consultations.FindAsync(command.ConsultationId);
           if (consultation is null)
           {
               throw new InvalidOperationException($"Consultation with ID {command.ConsultationId} not found.");
           }
           //consultation.SetWeight(command.Weight);
           await context.SaveChangesAsync();
        }

        public async Task Handle(AdministerDrugCommand command)
        {
            var consultation = await context.Consultations.FindAsync(command.ConsultationId);
            if (consultation is null)
            {
                throw new InvalidOperationException($"Consultation with ID {command.ConsultationId} not found.");
            }
            //consultation.AdministerDrug(command.DrugId, new Domain.ValueObjects.Dose(command.Quantity, command.UnitOfMeasure));
            await context.SaveChangesAsync();
        }

        public async Task Handle(RegisterVitalSignsCommand command)
        {
            var consultation = await context.Consultations.FindAsync(command.ConsultationId);
            if (consultation is null)
            {
                throw new InvalidOperationException($"Consultation with ID {command.ConsultationId} not found.");
            }
            //consultation.RegisterVitalSigns(command.VitalSignsReadings
            //                                       .Select(r => new VitalSigns(r.ReadingDateTime, 
            //                                                                   r.Temperature, 
            //                                                                   r.HeartRate, 
            //                                                                   r.RespiratoryRate)));
            await context.SaveChangesAsync();
        }

        public async Task SaveAsync(Consultation consultation)
        {
            var aggregateId = $"Consultation-{consultation.Id}";
            var changes = consultation.GetChanges()
                .Select(e =>
                new ConsultationEventData(Guid.NewGuid(),
                                                       aggregateId,
                                                       e.GetType().Name,
                                                       JsonConvert.SerializeObject(e),
                                                       e.GetType().AssemblyQualifiedName));
            if (!changes.Any())
            {
                return;
            }

            foreach (var change in changes)
            {
                await context.Consultations.AddAsync(change);
            }
            await context.SaveChangesAsync();

            consultation.ClearChanges();
        }

        public async Task<Consultation> LoadAsync(Guid id)
        {
            var aggregateId = $"Consultation-{id}";
            var result = await context.Consultations
                .Where(e => e.AggregateId == aggregateId)
                .ToListAsync();

            var domainEvents = result.Select(e =>
            {
                var assemblyQualifiedName = e.AssemblyQualifiedName;
                var eventType = Type.GetType(assemblyQualifiedName);
                var data = JsonConvert.DeserializeObject(e.Data, eventType!);

                return data as IDomainEvent;
            });

            var aggregate = new Consultation(domainEvents!);

            return aggregate;
        }
    }
}
