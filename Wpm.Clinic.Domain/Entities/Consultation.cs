using Wpm.Clinic.Domain.Events;
using Wpm.Clinic.Domain.ValueObjects;
using Wpm.SharedKernel;
using Wpm.SharedKernel.DomainEvents;

namespace Wpm.Clinic.Domain.Entities
{
    // aggregate root class
    public class Consultation : AggregateRoot 
    {
        private readonly List<DrugAdministration> administeredDrugs = new();
        private readonly List<VitalSigns> vitalSignsReading = new();
       public DateTimeRange When { get; private set; }
        public PatientId PatientId { get; private set; }
        public Text? Diagnosis { get; private set; }
        public Text? Treatment { get; private set; }
        public Weight? CurrentWeight { get; private set; }
        public ConsulttionStatus Status { get; private set; }
        public IReadOnlyCollection<DrugAdministration> AdminsteredDrug => administeredDrugs;
        public IReadOnlyCollection<VitalSigns> VitalSignsReading => vitalSignsReading;

        public Consultation(PatientId patientId)
        {
            ApplyDomainEvent(new ConsultationStarted(Guid.NewGuid(),
                                                     patientId,
                                                     DateTime.UtcNow));
        }

        public Consultation(IEnumerable<IDomainEvent> domainEvents)
        {
            Load(domainEvents);
        }

        public void RegisterVitalSigns(IEnumerable<VitalSigns> vitalSigns)
        {
            ValidateConsultationStatus();
            vitalSignsReading.AddRange(vitalSigns);
        }
        public void AdministerDrug(DrugId drugId, Dose dose)
        {
            ValidateConsultationStatus();
            var newDrugAdministration = new DrugAdministration(drugId, dose);
            // track drug administration
            administeredDrugs.Add(newDrugAdministration);
        }

        public void SetWeight(Weight weight)
        {
            ApplyDomainEvent(new WeightUpdated(Id, weight));
        }

        public void End()
        {
            ApplyDomainEvent(new ConsultationEnded(Id, DateTime.UtcNow));
        }

        public void SetDiagnosis(Text diagnosis) // dont allow any modifications after consultation is closed.
        {
            ApplyDomainEvent(new DiagnosisUpdated(Id, diagnosis));
        }

        public void SetTreatment(Text treatment)
        {
            ApplyDomainEvent(new TreatmentUpdated(Id, treatment));
        }

        private void ValidateConsultationStatus()
        {
            if(Status == ConsulttionStatus.Closed)
            {
                throw new InvalidOperationException("The consultation is already closed");
            }
        }

        public override void ChangeStateByUsingDomainEvent(IDomainEvent domainEvent)
        {
            // validate the domain event type and update the state of the aggregate accordingly
            switch (domainEvent)
            {
                case ConsultationStarted consultationStarted:
                    Id = consultationStarted.Id;
                    PatientId = consultationStarted.PatientId;
                    When = consultationStarted.StartedAt;
                    Status = ConsulttionStatus.Open;
                    break;
                case DiagnosisUpdated diagnosisUpdated:
                    ValidateConsultationStatus();
                    Diagnosis = diagnosisUpdated.Diagnosis;
                    break;
                case WeightUpdated weightUpdated:
                    ValidateConsultationStatus();
                    CurrentWeight = weightUpdated.Weight;
                    break;
                case TreatmentUpdated treatmentUpdated:
                    ValidateConsultationStatus();
                    Treatment = treatmentUpdated.Treatment;
                    break;
                case ConsultationEnded consultationEnded:
                    ValidateConsultationStatus();
                    if (Diagnosis == null || Treatment == null || CurrentWeight == null)
                    {
                        throw new InvalidOperationException("The consultation cannot be ended");

                    }
                    Status = ConsulttionStatus.Closed;
                    When = new DateTimeRange(When.StartedAt, DateTime.UtcNow);
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported domain event type: {domainEvent.GetType().Name}");
            }
        }
    }

    public enum ConsulttionStatus
    {
        Open,
        Closed
    }
}
