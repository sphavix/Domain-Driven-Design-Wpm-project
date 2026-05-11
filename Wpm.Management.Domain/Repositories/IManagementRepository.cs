using System;
using System.Collections.Generic;
using System.Text;
using Wpm.Management.Domain.Entities;

namespace Wpm.Management.Domain.Repositories
{
    public interface IManagementRepository
    {
        Pet? GetById(Guid id);
        IEnumerable<Pet> GetPets();
        void Insert(Pet pet);
        void Update(Pet pet);
        void Delete(Pet pet);
    }
}
