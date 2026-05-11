using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.Services;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Api.Infrastructure
{
    public class BreedService : IBreedService
    {
        public readonly List<Breed> breeds = [
              new Breed (Guid.Parse("4df1d2c9-32ef-4e56-a521-c57d54bec999"), "Beegle", new WeightRange(10m, 20m), new WeightRange(10m, 20m)),
              new Breed (Guid.Parse("38616925-35ef-4111-84a5-2549a169cafd"), "Staffordshire Terrier", new WeightRange(10m, 25m), new WeightRange(10m, 25m)),
            ];
        public Breed? GetBreed(Guid id)
        {
           if(id == Guid.Empty)
            {
                throw new ArgumentException("Breed is not valid.");
            }

           var result = breeds.Find(breeds => breeds.Id == id);
            return result ?? throw new ArgumentException("Breed was not found.");
        }
    }
}
