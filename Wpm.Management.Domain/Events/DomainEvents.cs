using System;
using System.Collections.Generic;
using System.Text;
using Wpm.SharedKernel.DomainEvents;

namespace Wpm.Management.Domain.Events
{
    public static class DomainEvents
    {
        public static DomainEventDispatcher<PetWeightUpdated> PetWeightUpdated { get; } = new();
    }
}
