namespace PetSitter.Domain.Interface;

using PetSitter.Domain.Models;


public interface IPetRepository
{
    public List<Pet> GetPets();
    public Pet? GetPet(Guid petId);
    public Pet CreatePet(PetBase petInput);
    public Pet? UpdatePet(Guid petId, PetBase petInput);
    public void DeletePet(Guid petId);
}