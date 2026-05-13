namespace Wpm.Clinic.Api.Application.Commands
{
    public record SetDiagnosisCommand(Guid ConsultationId, string Diagnosis);

}
