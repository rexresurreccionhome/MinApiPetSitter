// namespace PetSitter.Tests.Repository;


// using PetSitter.Models;
// using PetSitter.Repository;


// public class PetRepositoryTests
// {

//     [Fact]
//     public void GetPets_WithPetsExists_ShouldReturnListOfPets()
//     {
//         // Arrange
//         List<Pet> pets = [
//             new Pet(){PetId=new Guid("aededee5-1ba2-419b-8d86-c1965d02f89d"), Name="Max"},
//             new Pet(){PetId=new Guid("3bfe342b-5a3b-46ed-a13b-08c72f871489"), Name="Daisy"},
//         ];
//         PetRepository petRepository = new(pets);
//         // Act
//         List<Pet> listOfPets = petRepository.GetPets();
//         // Assert
//         Assert.Same(listOfPets, pets);
//     }

//     [Fact]
//     public void GetPet_WithPetExists_ShouldReturnPet()
//     {
//         // Arrange
//         Guid petId = new("aededee5-1ba2-419b-8d86-c1965d02f89d");
//         List<Pet> pets = [
//             new Pet(){PetId=petId, Name="Max"},
//         ];
//         PetRepository petRepository = new(pets);
//         // Act
//         Pet? pet = petRepository.GetPet(petId);
//         // Assert
//         Assert.NotNull(pet);
//         Assert.Same(pet, pets[0]);
//     }

//     [Fact]
//     public void CreatePet_NewPet_ShouldReturnNewPet()
//     {
//         // Arrange
//         PetInput petInput = new() { Name = "Max" };
//         PetRepository petRepository = new();
//         // Act
//         Pet newPet = petRepository.CreatePet(petInput);
//         // Assert
//         Assert.Equal(newPet.Name, petInput.Name);
//         Assert.IsType<Guid>(newPet.PetId);
//     }

//     [Fact]
//     public void UpdatePet_WithExistingPetToUpdate_ShouldReturnUpdatedPet()
//     {
//         // Arrange
//         Guid petId = new("aededee5-1ba2-419b-8d86-c1965d02f89d");
//         List<Pet> pets = [
//             new Pet(){PetId=petId, Name="Max"},
//             new Pet(){PetId=new Guid("3bfe342b-5a3b-46ed-a13b-08c72f871489"), Name="Daisy"},
//         ];
//         PetInput petInput = new() { Name = "Spot" };
//         PetRepository petRepository = new(pets);
//         // Act
//         Pet? pet = petRepository.UpdatePet(petId, petInput);
//         // Assert
//         Assert.NotNull(pet);
//         Assert.Equal(pet.PetId, petId);
//         Assert.Equal(pet.Name, petInput.Name);
//     }

//     [Fact]
//     public void DeletePet_WithExistingPetId_ShouldRemoveFromListOfPets()
//     {
//         // Arrange
//         Guid petId = new("aededee5-1ba2-419b-8d86-c1965d02f89d");
//         List<Pet> pets = [
//             new Pet(){PetId=petId, Name="Max"},
//             new Pet(){PetId=new Guid("3bfe342b-5a3b-46ed-a13b-08c72f871489"), Name="Daisy"},
//             new Pet(){PetId=new Guid("179a2d6e-3549-4a8f-92e2-d5fdf3ed0047"), Name="Red"},
//         ];
//         PetRepository petRepository = new(pets);
//         // Act
//         petRepository.DeletePet(petId);
//         List<Pet> remainingPets = petRepository.GetPets();
//         // Assert
//         Assert.Equal(2, remainingPets.Count);
//         Pet? notFoundPet = remainingPets.Find(pet => pet.PetId == petId);
//         Assert.Null(notFoundPet);
//     }
// }