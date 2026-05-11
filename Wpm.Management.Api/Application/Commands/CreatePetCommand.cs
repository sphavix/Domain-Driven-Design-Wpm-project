using System.Globalization;
using Wpm.Management.Domain.Entities;

namespace Wpm.Management.Api.Application.Commands
{
    public record CreatePetCommand(Guid Id, string Name, int Age, string Color, SexOfPet SexOfPet, Guid BreedId);
}
