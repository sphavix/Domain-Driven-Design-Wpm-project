using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.Repositories;

namespace Wpm.Management.Api.Infrastructure.Repositories
{
    public class ManagementRepository(ManagementDbContext context) : IManagementRepository
    {
        private readonly ManagementDbContext _context = context;

        public void Delete(Pet pet)
        {
            throw new NotImplementedException();
        }

        public Pet? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pet> GetPets()
        {
            throw new NotImplementedException();
        }

        public void Insert(Pet pet)
        {
            throw new NotImplementedException();
        }

        public void Update(Pet pet)
        {
            throw new NotImplementedException();
        }
    }
}
