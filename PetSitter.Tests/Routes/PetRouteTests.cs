namespace PetSitter.Tests.Routes;

using Xunit;
using Moq;
using Microsoft.AspNetCore.Http.HttpResults;

using PetSitter.Domain.Models;
using PetSitter.Domain.Interface;
using PetSitter.Models;
using PetSitter.Routes;


public class PetHandlerTests
{
    [Fact]
    public void GetPets_WithListOfPets_ShouldReturnTypedResultsOk()
    {
        // Arrange
        List<Pet> pets = [new Pet { Name = "Lola" }, new Pet { Name = "Cooper" }];
        Mock<IPetRepository> mockPetRepository = new();
        mockPetRepository.Setup(petRepository => petRepository.GetPets()).Returns(value: pets);
        PetRoute.PetHandler petHandler = new();
        // Act
        Ok<GetPetsResponse> result = petHandler.GetPets(mockPetRepository.Object);
        // Assert
        Assert.IsType<Ok<GetPetsResponse>>(result);
        Assert.NotNull(result.Value);
        Assert.Same(result.Value.Pets, pets);
    }

    [Fact]
    public void GetPet_WithExistingPetId_ShouldReturnTypedResultsOk()
    {
        // Arrange
        Pet pet = new() { PetId = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"), Name = "Lola" };
        Mock<IPetRepository> mockPetRepository = new();
        mockPetRepository.Setup(petRepository => petRepository.GetPet(pet.PetId)).Returns(value: pet);
        PetRoute.PetHandler petHandler = new();
        // Act
        Results<Ok<Pet>, NotFound> result = petHandler.GetPet(pet.PetId, mockPetRepository.Object);
        // Assert
        Assert.IsType<Ok<Pet>>(result.Result);
    }

    [Fact]
    public void GetPet_PetIdDoesNotExist_ShouldReturnTypedResultsNotFound()
    {
        // Arrange
        Guid petId = new("9D2B0228-4D0D-4C23-8B49-01A698857709");
        Mock<IPetRepository> mockPetRepository = new();
        mockPetRepository.Setup(petRepository => petRepository.GetPet(petId)).Returns(value: null);
        PetRoute.PetHandler petHandler = new();
        // Act
        Results<Ok<Pet>, NotFound> result = petHandler.GetPet(petId, mockPetRepository.Object);
        // Assert
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public void CreatePet_NewPet_ShouldReturnTypedResultsCreated()
    {
        // Arrange
        PetInput petInput = new() { Name = "Lola" };
        Pet pet = new() { Name = petInput.Name };
        Mock<IPetRepository> mockPetRepository = new();
        mockPetRepository.Setup(petRepository => petRepository.CreatePet(petInput)).Returns(pet);
        PetRoute.PetHandler petHandler = new();
        // Act
        Results<Created<Pet>, Created> result = petHandler.CreatePet(petInput, mockPetRepository.Object);
        // Assert
        Assert.IsType<Created<Pet>>(result.Result);
    }

    [Fact]
    public void UpdatePet_WithExistingPetToUpdate_ShouldReturnTypedResultsOk()
    {
        // Arrange
        Guid petId = new("9D2B0228-4D0D-4C23-8B49-01A698857709");
        PetInput petInput = new() { Name = "Max" };
        Pet updatedPet = new() { PetId = petId, Name = petInput.Name };
        Mock<IPetRepository> mockPetRepository = new();
        mockPetRepository.Setup(petRepository => petRepository.UpdatePet(petId, petInput)).Returns(value: updatedPet);
        PetRoute.PetHandler petHandler = new();
        // Act
        Results<Ok<Pet>, NotFound> result = petHandler.UpdatePet(petId, petInput, mockPetRepository.Object);
        // Assert
        Assert.IsType<Ok<Pet>>(result.Result);
    }

    [Fact]
    public void UpdatePet_PetToUpdateDoesNotExist_ShouldReturnTypedResultsOk()
    {
        // Arrange
        Guid petId = new("9D2B0228-4D0D-4C23-8B49-01A698857709");
        PetInput petInput = new() { Name = "Max" };
        Mock<IPetRepository> mockPetRepository = new();
        mockPetRepository.Setup(petRepository => petRepository.UpdatePet(petId, petInput)).Returns(value: null);
        PetRoute.PetHandler petHandler = new();
        // Act
        Results<Ok<Pet>, NotFound> result = petHandler.UpdatePet(petId, petInput, mockPetRepository.Object);
        // Assert
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public void DeletePet_WithExistingPetId_ShouldReturnTypedResultsEmpty()
    {
        // Arrange
        Guid petId = new("9D2B0228-4D0D-4C23-8B49-01A698857709");
        Mock<IPetRepository> mockPetRepository = new();
        mockPetRepository.Setup(petRepository => petRepository.DeletePet(petId));
        PetRoute.PetHandler petHandler = new();
        // Act
        Results<EmptyHttpResult, NoContent> result = petHandler.DeletePet(petId, mockPetRepository.Object);
        // Assert
        Assert.IsType<NoContent>(result.Result);
        mockPetRepository.Verify(mock => mock.DeletePet(petId), Times.Once());
    }

    [Fact]
    public void GetAppointments_WithAppointmentsAssociatedToPetId_ShouldReturnTypedResultsOk()
    {
        // Arrange
        Guid petId = new("9D2B0228-4D0D-4C23-8B49-01A698857709");
        List<Appointment> appointments = [
            new Appointment() {
                ServiceType=ServiceTypes.DogWalking,
                AppointmentDateTime=new DateTime(2025, 02, 09),
                PetId=petId,
                SitterId=new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709")
            },
            new Appointment() {
                ServiceType=ServiceTypes.HomeVisit,
                AppointmentDateTime=new DateTime(2025, 02, 10),
                PetId=petId,
                SitterId=new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709")
            }
        ];
        Mock<IAppointmentRepository> mockAppointmentRepository = new();
        mockAppointmentRepository.Setup(appointmentRepository => appointmentRepository.GetAppointmentsByPet(petId, null)).Returns(value: appointments);
        PetRoute.PetHandler petHandler = new();
        // Act
        Ok<GetApppointmentsResponse> result = petHandler.GetAppointments(petId, mockAppointmentRepository.Object);
        // Assert
        Assert.IsType<Ok<GetApppointmentsResponse>>(result);
        Assert.NotNull(result.Value);
        Assert.Same(result.Value.Appointments, appointments);
    }

    [Fact]
    public void GetAppointments_WithAppointmentsAssociatedToPetIdFilterByServiceType_ShouldReturnTypedResultsOk()
    {
        // Arrange
        Guid petId = new("9D2B0228-4D0D-4C23-8B49-01A698857709");
        List<Appointment> appointments = [
            new Appointment() {
                ServiceType=ServiceTypes.HomeVisit,
                AppointmentDateTime=new DateTime(2025, 02, 10),
                PetId=petId,
                SitterId=new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709")
            }
        ];
        Mock<IPetRepository> mockPetRepository = new();
        Mock<IAppointmentRepository> mockAppointmentRepository = new();
        mockAppointmentRepository.Setup(appointmentRepository => appointmentRepository.GetAppointmentsByPet(petId, ServiceTypes.HomeVisit)).Returns(value: appointments);
        PetRoute.PetHandler petHandler = new();
        // Act
        Ok<GetApppointmentsResponse> result = petHandler.GetAppointments(petId, mockAppointmentRepository.Object, serviceType: ServiceTypes.HomeVisit);
        // Assert
        Assert.IsType<Ok<GetApppointmentsResponse>>(result);
        Assert.NotNull(result.Value);
        Assert.Same(result.Value.Appointments, appointments);
    }
}