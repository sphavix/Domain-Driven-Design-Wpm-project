namespace Wpm.Clinic.Api.Application.Commands
{
    public record VitalSignsReading(DateTime ReadingDateTime, decimal Temperature, int HeartRate, int RespiratoryRate);

}
