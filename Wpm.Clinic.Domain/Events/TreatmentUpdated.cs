using Wpm.SharedKernel.DomainEvents;

namespace Wpm.Clinic.Domain.Events
{
    public record TreatmentUpdated(Guid Id, string Treatment) : IDomainEvent;
}
