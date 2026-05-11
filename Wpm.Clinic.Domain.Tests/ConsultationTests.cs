using Wpm.Clinic.Domain.Entities;
using Wpm.Clinic.Domain.ValueObjects;

namespace Wpm.Clinic.Domain.Tests;

public class ConsultationTests
{
    [Fact]
    public void Consultation_should_be_open()
    {
        var consultation = new Consultation(Guid.NewGuid());
        Assert.True(consultation.Status == ConsulttionStatus.Open);
    }

    [Fact]
    public void Consultation_should_not_have_ended_timestamp()
    {
        var consultation = new Consultation(Guid.NewGuid());
        Assert.Null(consultation.EndedAt);
    }

    [Fact]
    public void Consultation_should_not_end_when_missing_data()
    {
        var consultation = new Consultation(Guid.NewGuid());
        Assert.Throws<InvalidOperationException>(() => consultation.End());
    }

    [Fact]
    public void Consultation_should_end_with_complete_data()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetTreatment("Treatment");
        consultation.SetDiagnosis("Diagnosis");
        consultation.SetWeight(18.5m);
        consultation.End();
        Assert.True(consultation.Status == ConsulttionStatus.Closed);
    }

    [Fact]
    public void Consultation_should_not_allow_weight_updates_when_closed()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetTreatment("Treatment");
        consultation.SetDiagnosis("Diagnosis");
        consultation.SetWeight(18.5m);
        consultation.End();
        Assert.Throws<InvalidOperationException>(() => consultation.SetWeight(19.2m));
    }

    [Fact]
    public void Consultation_should_not_allow_diagnosis_updates_when_closed()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetTreatment("Treatment");
        consultation.SetDiagnosis("Diagnosis");
        consultation.SetWeight(18.5m);
        consultation.End();
        Assert.Throws<InvalidOperationException>(() => consultation.SetDiagnosis("New Diagnosis"));
    }

    [Fact]
    public void Consultation_should_not_allow_treatment_updates_when_closed()
    {
        var consultation = new Consultation(Guid.NewGuid());
        consultation.SetTreatment("Treatment");
        consultation.SetDiagnosis("Diagnosis");
        consultation.SetWeight(18.5m);
        consultation.End();
        Assert.Throws<InvalidOperationException>(() => consultation.SetTreatment("New Treatment"));
    }

    [Fact]
    public void Consultation_should_administer_drug()
    {
        var drugId = new DrugId(Guid.NewGuid());
        var consultation = new Consultation(Guid.NewGuid());
        consultation.AdministerDrug(drugId, new Dose(1, UnitOfMeasure.tablet));
        Assert.True(consultation.AdminsteredDrug.Count == 1);
        Assert.True(consultation.AdminsteredDrug.First().DrugId == drugId);
    }

    [Fact]
    public void Consultation_should_register_vitalsigns()
    {
        var consultation = new Consultation(Guid.NewGuid());
        IEnumerable<VitalSigns> vitalSigns = [new VitalSigns(38.8m, 100, 120)];
        consultation.RegisterVitalSigns(vitalSigns);
        Assert.True(consultation.VitalSignsReading.Count == 1);
        Assert.True(consultation.VitalSignsReading.First() == vitalSigns.First());
    }
}
