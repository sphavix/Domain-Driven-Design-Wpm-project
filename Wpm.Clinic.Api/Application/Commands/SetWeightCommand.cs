namespace Wpm.Clinic.Api.Application.Commands
{
    public record SetWeightCommand(Guid ConsultationId, decimal Weight);

}
