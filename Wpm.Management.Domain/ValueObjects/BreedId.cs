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

        private BreedId(Guid value)
        {
            Value =value;
        }

        public static BreedId Create(Guid value) // used in the db context, where we don't have access to the breed service
        {
            return new BreedId(value);
        }

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
