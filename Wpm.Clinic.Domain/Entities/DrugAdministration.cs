using System;
using System.Collections.Generic;
using System.Text;
using Wpm.Clinic.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain.Entities
{
    public class DrugAdministration : BaseEntity
    {
        public DrugId DrugId { get; init; }
        public Dose Dose { get; init; }

        public DrugAdministration(DrugId drugId, Dose dose)
        {
            Id = Guid.NewGuid();
            DrugId = drugId;
            Dose = dose;

        }
    }
}
