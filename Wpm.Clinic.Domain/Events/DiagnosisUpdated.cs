using Wpm.SharedKernel.DomainEvents;

namespace Wpm.Clinic.Domain.Events
{
    public record DiagnosisUpdated(Guid Id, string Diagnosis) : IDomainEvent;
}
