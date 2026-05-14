using Wpm.SharedKernel.DomainEvents;

namespace Wpm.Clinic.Domain.Events
{
    public record ConsultationEnded(Guid Id, DateTime EndedAt) : IDomainEvent;
}
