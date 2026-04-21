using System;
using System.Collections.Generic;
using System.Text;
using Wpm.Management.Domain.Services;

namespace Wpm.Management.Domain.ValueObjects
{
    public record BreedId
    {
        public readonly IBreedService breedService;
        public Guid Value {  get; set; }

        public BreedId(Guid value, IBreedService breedService)
        {
            this.breedService = breedService;

            ValidateBreed(value);
            Value = value;
        }

        private void ValidateBreed(Guid value)
        {
            if(breedService.GetBreed(value) == null)
            {
                throw new ArgumentException("Breed is not valid.");
            }
        }
    }
}
