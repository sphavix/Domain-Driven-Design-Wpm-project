using System;
using System.Collections.Generic;
using System.Text;
using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Domain.Services
{
    public class BreedService : IBreedService
    {
        public readonly List<Breed> breeds = [
              new Breed (Guid.NewGuid(), "Beegle", new WeightRange(10m, 20m), new WeightRange(10m, 20m)),
              new Breed (Guid.NewGuid(), "Staffordshire Terrier", new WeightRange(10m, 25m), new WeightRange(10m, 25m)),
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
