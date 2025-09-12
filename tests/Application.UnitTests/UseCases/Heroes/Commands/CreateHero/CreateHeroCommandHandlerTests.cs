using ImposterSyndrome.Application.UseCases.Heroes.Commands.CreateHero;
using ImposterSyndrome.Domain.Heroes;
using NSubstitute.ExceptionExtensions;
using System.Collections.Generic;

namespace ImposterSyndrome.Application.UnitTests.UseCases.Heroes.Commands.CreateHero;
public class CreateHeroCommandHandlerTests
{
    private CreateHeroCommandValidator _validator = new();
    private Faker _faker = new Faker();

    [Fact]
    public async Task Handle_ValidRequest_AddsHeroAndSavesChanges_ReturnsHeroId()
    {
        // Arrange
        var dbSet = Substitute.For<DbSet<Hero>, IQueryable<Hero>>();
        var dbContext = Substitute.For<IApplicationDbContext>();
        dbContext.Heroes.Returns(dbSet);
        dbContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

        dbSet.AddAsync(Arg.Any<Hero>(), Arg.Any<CancellationToken>())
            .Returns(ValueTask.FromResult((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Hero>)null!));

        var handler = new CreateHeroCommandHandler(dbContext);

        var powers = new List<CreateHeroPowerDto>
        {
            new("Flight", 10),
            new("Strength", 8)
        };

        var command = new CreateHeroCommand("Clark Kent", "Superman", powers);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        await dbSet.Received(1).AddAsync(Arg.Any<Hero>(), Arg.Any<CancellationToken>());
        await dbContext.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());

//        Assert.True(result.IsOk);
        Assert.IsType<Guid>(result.Value);
    }

    [Fact]
    public async Task Handle_SaveChangesAsyncFails_ThrowsException()
    {
        // Arrange
        var dbSet = Substitute.For<DbSet<Hero>, IQueryable<Hero>>();
        var dbContext = Substitute.For<IApplicationDbContext>();
        dbContext.Heroes.Returns(dbSet);
        dbContext.SaveChangesAsync(Arg.Any<CancellationToken>())
            .ThrowsAsync(new DbUpdateException("Save failed"));

        dbSet.AddAsync(Arg.Any<Hero>(), Arg.Any<CancellationToken>())
            .Returns(ValueTask.FromResult((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Hero>)null!));

        var handler = new CreateHeroCommandHandler(dbContext);

        var powers = new List<CreateHeroPowerDto>
        {
            new("Invisibility", 5)
        };

        var command = new CreateHeroCommand("Bruce Wayne", "Batman", powers);

        // Act & Assert
        await Assert.ThrowsAsync<DbUpdateException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var command = new CreateHeroCommand(
            "",
            _faker.Lorem.Word(),
            new List<CreateHeroPowerDto>()
        );

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Alias_Is_Empty()
    {
        var command = new CreateHeroCommand(
            _faker.Lorem.Word(),
            "",
            new List<CreateHeroPowerDto>()
        );

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Alias);
    }

    [Fact]
    public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
    {
        var powers = new Faker<CreateHeroPowerDto>()
            .CustomInstantiator(f => new CreateHeroPowerDto(
                f.Lorem.Word(),
                f.Random.Int(1, 10)
            ))
            .Generate(3);

        var command = new CreateHeroCommand(
            _faker.Lorem.Word(),
            _faker.Lorem.Word(),
            powers
        );

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
