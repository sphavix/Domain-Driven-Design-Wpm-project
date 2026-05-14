using Microsoft.EntityFrameworkCore;
using Wpm.Management.Api.Application.Commands;
using Wpm.Management.Api.Application.Services;
using Wpm.Management.Api.Infrastructure;
using Wpm.Management.Domain.Events;
using Wpm.Management.Domain.Services;

namespace Wpm.Management.Api.Application.Handlers
{
    public class SetWeightCommandHandler : ICommandHandler<SetWeightCommand>
    {
        private readonly ManagementDbContext _context;
        private readonly IBreedService _breedService;
        public SetWeightCommandHandler(ManagementDbContext context,
                                         IBreedService breedService)
        {
            _context = context;
            _breedService = breedService;

            DomainEvents.PetWeightUpdated.Subscribe((domainEvent) =>
            {
                // Send a notification to the user that the pet's weight has been updated. This can be used in the MessageBus.
                
            });
        }
        public async Task Handle(SetWeightCommand command)
        {
            var pet = await _context.Pets.FindAsync(command.Id);
            if (pet is null)
            {
                throw new Exception($"Pet with id {command.Id} not found.");
            }
            pet.SetWeight(command.Weight, _breedService);
            await _context.SaveChangesAsync();
        }
    }
}
