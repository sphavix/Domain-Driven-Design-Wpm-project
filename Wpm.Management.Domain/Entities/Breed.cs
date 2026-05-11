using Wpm.Management.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Management.Domain.Entities
{
    public class Breed : BaseEntity
    {
        public string Name { get; set; }
        public WeightRange MaleWeightRange {  get; set; }
        public WeightRange FemaleWeightRange { get; set; }

        public Breed(Guid id, string name, WeightRange maleWeightRange, WeightRange femaleWeightRange)
        {
            Id = id;
            Name = name;
            MaleWeightRange = maleWeightRange;
            FemaleWeightRange = femaleWeightRange;
        }


    }
}
