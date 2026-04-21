using System;
using System.Collections.Generic;
using System.Text;
using Wpm.Management.Domain.Entities;

namespace Wpm.Management.Domain.Services
{
    public interface IBreedService
    {
        Breed? GetBreed(Guid id);
    }
}
