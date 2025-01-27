namespace PetSitter.Repository;

using PetSitter.Models;
using PetSitter.DB;


public class PetRepository: IPetRepository {
    public PetRepository(List<Pet> pets) {
        PetDB.Pets = pets;
    }

    public List<Pet> GetPets() => PetDB.Pets;

    public Pet? GetPet(Guid petId) => PetDB.Pets.Find(pet => pet.PetId == petId);

    public Pet CreatePet(PetInput petInput) {
        PetDB.Pets.Add(new Pet{Name=petInput.Name});
        return PetDB.Pets.Last();
    }

    public Pet? UpdatePet(Guid petId, PetInput petInput) {
        foreach(Pet pet in PetDB.Pets) {
            if(pet.PetId == petId) {
                pet.Name = petInput.Name;
            }
        }
        return PetDB.Pets.Find(pet => pet.PetId == petId);
    }

    public void DeletePet(Guid petId) {
        foreach(Pet pet in PetDB.Pets.ToList()) {
            if(pet.PetId == petId) {
                PetDB.Pets.Remove(pet);
            }
        }
    }
}

