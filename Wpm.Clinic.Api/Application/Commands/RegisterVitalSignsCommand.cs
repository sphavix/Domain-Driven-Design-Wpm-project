namespace Wpm.Clinic.Api.Application.Commands
{
    public record RegisterVitalSignsCommand(Guid ConsultationId, IEnumerable<VitalSignsReading> VitalSignsReadings);

}
