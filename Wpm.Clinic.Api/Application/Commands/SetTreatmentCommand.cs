namespace Wpm.Clinic.Api.Application.Commands
{
    public record SetTreatmentCommand(Guid ConsultationId, string Treatment);

}
