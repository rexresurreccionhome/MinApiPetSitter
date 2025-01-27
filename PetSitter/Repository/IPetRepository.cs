namespace PetSitter.Repository;

using PetSitter.Models;


public interface IPetRepository {
     public List<Pet> GetPets();
     public Pet? GetPet(Guid petId);
     public Pet CreatePet(PetInput petInput);
     public Pet? UpdatePet(Guid petId, PetInput petInput);
     public void DeletePet(Guid petId);
}