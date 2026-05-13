using Wpm.Clinic.Domain.ValueObjects;

namespace Wpm.Clinic.Api.Application.Commands
{
    public record AdministerDrugCommand(Guid ConsultationId, Guid DrugId, decimal Quantity, UnitOfMeasure UnitOfMeasure);

}
