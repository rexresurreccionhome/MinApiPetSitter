namespace PetSitter.Tests.Routes;

using Xunit;
using Moq;
using Microsoft.AspNetCore.Http.HttpResults;

using PetSitter.Domain.Models;
using PetSitter.Domain.Interface;
using PetSitter.Models;
using PetSitter.Routes;


public class SitterHandlerTests
{
    [Fact]
    public void GetSitters_WithListOfSitters_ShouldReturnTypedResultsOk()
    {
        // Arrange
        List<Sitter> sitters = [new Sitter { Name = "Vicky" }, new Sitter { Name = "Smith" }];
        Mock<ISitterRepository> mockSitterRepository = new();
        mockSitterRepository.Setup(sitterRepository => sitterRepository.GetSitters()).Returns(value: sitters);
        SitterRoute.SitterHandler sitterHandler = new(mockSitterRepository.Object);
        // Act
        Ok<GetSittersResponse> result = sitterHandler.GetSitters();
        // Assert
        Assert.IsType<Ok<GetSittersResponse>>(result);
        Assert.NotNull(result.Value);
        Assert.Same(result.Value.Sitters, sitters);
    }

    [Fact]
    public void GetSitter_WithExistingSitterId_ShouldReturnTypedResultsOk()
    {
        // Arrange
        Sitter sitter = new() { SitterId = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"), Name = "Vicky" };
        Mock<ISitterRepository> mockSitterRepository = new();
        mockSitterRepository.Setup(sitterRepository => sitterRepository.GetSitter(sitter.SitterId)).Returns(value: sitter);
        SitterRoute.SitterHandler sitterHandler = new(mockSitterRepository.Object);
        // Act
        Results<Ok<Sitter>, NotFound> result = sitterHandler.GetSitter(sitter.SitterId);
        // Assert
        Assert.IsType<Ok<Sitter>>(result.Result);
    }

    [Fact]
    public void GetSitter_SitterIdDoesNotExist_ShouldReturnTypedResultsNotFound()
    {
        // Arrange
        Guid sitterId = new("9D2B0228-4D0D-4C23-8B49-01A698857709");
        Mock<ISitterRepository> mockSitterRepository = new();
        mockSitterRepository.Setup(sitterRepository => sitterRepository.GetSitter(sitterId)).Returns(value: null);
        SitterRoute.SitterHandler sitterHandler = new(mockSitterRepository.Object);
        // Act
        Results<Ok<Sitter>, NotFound> result = sitterHandler.GetSitter(sitterId);
        // Assert
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public void CreateSitter_NewSitter_ShouldReturnTypedResultsCreated()
    {
        // Arrange
        SitterInput sitterInput = new() { Name = "Vicky" };
        Sitter sitter = new() { Name = sitterInput.Name };
        Mock<ISitterRepository> mockSitterRepository = new();
        mockSitterRepository.Setup(sitterRepository => sitterRepository.CreateSitter(sitterInput)).Returns(value: sitter);
        SitterRoute.SitterHandler sitterHandler = new(mockSitterRepository.Object);
        // Act
        Results<Created<Sitter>, Created> result = sitterHandler.Createsitter(sitterInput);
        // Assert
        Assert.IsType<Created<Sitter>>(result.Result);
    }

    [Fact]
    public void UpdateSitter_WithExistingSitterToUpdate_ShouldReturnTypedResultsOk()
    {
        // Arrange
        Guid sitterId = new("9D2B0228-4D0D-4C23-8B49-01A698857709");
        SitterInput sitterInput = new() { Name = "Max" };
        Sitter updatedSitter = new() { SitterId = sitterId, Name = sitterInput.Name };
        Mock<ISitterRepository> mockSitterRepository = new();
        mockSitterRepository.Setup(sitterRepository => sitterRepository.UpdateSitter(sitterId, sitterInput)).Returns(value: updatedSitter);
        SitterRoute.SitterHandler sitterHandler = new(mockSitterRepository.Object);
        // Act
        Results<Ok<Sitter>, NotFound> result = sitterHandler.UpdateSitter(sitterId, sitterInput);
        // Assert
        Assert.IsType<Ok<Sitter>>(result.Result);
    }

    [Fact]
    public void UpdateSitter_SitterToUpdateDoesNotExist_ShouldReturnTypedResultsOk()
    {
        // Arrange
        Guid sitterId = new("9D2B0228-4D0D-4C23-8B49-01A698857709");
        SitterInput sitterInput = new() { Name = "Max" };
        Mock<ISitterRepository> mockSitterRepository = new();
        mockSitterRepository.Setup(sitterRepository => sitterRepository.UpdateSitter(sitterId, sitterInput)).Returns(value: null);
        SitterRoute.SitterHandler sitterHandler = new(mockSitterRepository.Object);
        // Act
        Results<Ok<Sitter>, NotFound> result = sitterHandler.UpdateSitter(sitterId, sitterInput);
        // Assert
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public void DeleteSitter_WithExistingSitterId_ShouldReturnTypedResultsEmpty()
    {
        // Arrange
        Guid sitterId = new("9D2B0228-4D0D-4C23-8B49-01A698857709");
        Mock<ISitterRepository> mockSitterRepository = new();
        mockSitterRepository.Setup(sitterRepository => sitterRepository.DeleteSitter(sitterId));
        SitterRoute.SitterHandler sitterHandler = new(mockSitterRepository.Object);
        // Act
        Results<EmptyHttpResult, NoContent> result = sitterHandler.DeleteSitter(sitterId);
        // Assert
        Assert.IsType<NoContent>(result.Result);
        mockSitterRepository.Verify(mock => mock.DeleteSitter(sitterId), Times.Once());
    }
}