using System;
using System.Collections.Generic;
using System.Text;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain.Entities
{
    public class Drug : BaseEntity
    {
        public string Name { get; init; }
    }
}
