using Microsoft.EntityFrameworkCore;
using Wpm.Management.Api.Application.Commands;
using Wpm.Management.Api.Application.Services;
using Wpm.Management.Api.Infrastructure;
using Wpm.Management.Domain.Services;

namespace Wpm.Management.Api.Application.Handlers
{
    public class SetWeightCommandHandler(ManagementDbContext context,
                                         IBreedService breedService) : ICommandHandler<SetWeightCommand>
    {
        public async Task Handle(SetWeightCommand command)
        {
            var pet = await context.Pets.FindAsync(command.Id);
            if (pet is null)
            {
                throw new Exception($"Pet with id {command.Id} not found.");
            }
            pet.SetWeight(command.Weight, breedService);
            await context.SaveChangesAsync();
        }
    }
}
