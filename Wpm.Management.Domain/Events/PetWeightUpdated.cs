
using Wpm.SharedKernel.DomainEvents;

namespace Wpm.Management.Domain.Events
{
    public record PetWeightUpdated(Guid Id, decimal Weight) : IDomainEvent;
}
