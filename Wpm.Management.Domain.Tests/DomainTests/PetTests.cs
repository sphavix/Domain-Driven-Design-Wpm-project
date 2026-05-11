using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.Services;
using Wpm.Management.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Management.Domain.Tests.DomainTests;

public class PetTests
{
    [Fact]
    public void Pet_Should_be_Equal()
    {
        var id = Guid.NewGuid();
        var breedService = new BreedService();
        var breedId = new BreedId(breedService.breeds[0].Id, breedService);
        var pet1 = new Pet(id, "Gianni", 12, "Three-color", SexOfPet.Male, breedId);
        var pet2 = new Pet(id, "Karti", 7, "Black", SexOfPet.Female, breedId);

        Assert.True(pet1.Equals(pet2));
    }

    [Fact]
    public void Pet_Should_be_Equal_using_operators()
    {
        var id = Guid.NewGuid();
        var breedService = new BreedService();
        var breedId = new BreedId(breedService.breeds[0].Id, breedService);
        var pet1 = new Pet(id, "Gianni", 12, "Three-color", SexOfPet.Male, breedId);
        var pet2 = new Pet(id, "Karti", 7, "Black", SexOfPet.Female, breedId);

        Assert.True(pet1 == pet2);
    }

    [Fact]
    public void Pet_Should_not_be_equal_using_operators()
    {
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var breedService = new BreedService();
        var breedId = new BreedId(breedService.breeds[0].Id, breedService);
        var pet1 = new Pet(id1, "Gianni", 12, "Three-color", SexOfPet.Male, breedId);
        var pet2 = new Pet(id2, "Karti", 7, "Black", SexOfPet.Female, breedId);

        Assert.True(pet1 != pet2);
    }

    [Fact]
    public void Weight_Shuld_be_equal()
    {
        var w1 = new Weight(20.5m);
        var w2 = new Weight(20.5m);

        Assert.True(w1 == w2);
    }

    [Fact]
    public void WeightRange_Shuld_be_equal()
    {
        var wr1 = new WeightRange(10.5m, 20.5m);
        var wr2 = new WeightRange(10.5m, 20.5m);

        Assert.True(wr1 == wr2);


    }

    [Fact]
    public void BreedId_Should_be_Valid()
    {
        var breedService = new BreedService();
        var id = breedService.breeds[0].Id;
        var breedId = new BreedId(id, breedService);
        Assert.NotNull(breedId);
    }

    [Fact]
    public void BreedId_Should_not_be_Valid()
    {
        var breedService = new BreedService();
        var id = Guid.NewGuid();
        Assert.Throws<ArgumentException>(() =>
        {
            var breedId = new BreedId(id, breedService);
        });
    }

    [Fact]
    public void WeightClass_Should_be_Ideal()
    {
        var breedService = new BreedService();
        var breedId = new BreedId(breedService.breeds[0].Id, breedService);
        var pet = new Pet(Guid.NewGuid(), "Gianni", 12, "Three-color", SexOfPet.Male, breedId);
        pet.SetWeight(10, breedService);
        Assert.True(pet.WeightClass == WeightClass.Ideal);
    }

    [Fact]
    public void WeightClass_Should_be_UnderWeight()
    {
        var breedService = new BreedService();
        var breedId = new BreedId(breedService.breeds[0].Id, breedService);
        var pet = new Pet(Guid.NewGuid(), "Gianni", 12, "Three-color", SexOfPet.Male, breedId);
        pet.SetWeight(8, breedService);
        Assert.True(pet.WeightClass == WeightClass.Underweight);
    }

    [Fact]
    public void WeightClass_Should_be_OverWeight()
    {
        var breedService = new BreedService();
        var breedId = new BreedId(breedService.breeds[0].Id, breedService);
        var pet = new Pet(Guid.NewGuid(), "Gianni", 12, "Three-color", SexOfPet.Male, breedId);
        pet.SetWeight(27, breedService);
        Assert.True(pet.WeightClass == WeightClass.Overweight);
    }
}
