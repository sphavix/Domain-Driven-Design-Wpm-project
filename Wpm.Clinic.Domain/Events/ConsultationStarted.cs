using System;
using System.Collections.Generic;
using System.Text;
using Wpm.SharedKernel.DomainEvents;

namespace Wpm.Clinic.Domain.Events
{
    public record ConsultationStarted(Guid Id, Guid PatientId, DateTime StartedAt): IDomainEvent;
}
