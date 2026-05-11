using Wpm.Clinic.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain.Entities
{
    // aggregate root class
    public class Consultation : AggregateRoot 
    {
        private readonly List<DrugAdministration> administeredDrugs = new();
        private readonly List<VitalSigns> vitalSignsReading = new();
        public DateTime StartedAt { get; init; }
        public DateTime? EndedAt { get; private set; }
        public PatientId PatientId { get; init; }
        public Text Diagnosis { get; private set; }
        public Text Treatment { get; private set; }
        public Weight CurrentWeight { get; private set; }
        public ConsulttionStatus Status { get; private set; }
        public IReadOnlyCollection<DrugAdministration> AdminsteredDrug => administeredDrugs;
        public IReadOnlyCollection<VitalSigns> VitalSignsReading => vitalSignsReading;

        public Consultation(PatientId patientId)
        {
            Id = Guid.NewGuid();
            PatientId = patientId;
            Status = ConsulttionStatus.Open;
            StartedAt = DateTime.UtcNow;
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
            ValidateConsultationStatus();
            CurrentWeight = weight;
        }

        public void End()
        {
            ValidateConsultationStatus();
            if(Diagnosis == null || Treatment == null || CurrentWeight == null)
            {
                throw new InvalidOperationException("The consultation cannot be ended");

            }

            Status = ConsulttionStatus.Closed;
            EndedAt = DateTime.UtcNow;
        }

        public void SetDiagnosis(Text diagnosis) // dont allow any modifications after consultation is closed.
        {
            ValidateConsultationStatus();
            Diagnosis = diagnosis;
        }

        public void SetTreatment(Text treatment)
        {
            ValidateConsultationStatus();
            Treatment = treatment;
        }

        private void ValidateConsultationStatus()
        {
            if(Status == ConsulttionStatus.Closed)
            {
                throw new InvalidOperationException("The consultation is already closed");
            }
        }
    }

    public enum ConsulttionStatus
    {
        Open,
        Closed
    }
}
