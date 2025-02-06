
namespace PetSitter.Models;

using PetSitter.Domain.Models;


public class PetInput: PetBase;

public class GetPetsResponse {
    public required List<Pet> Pets {get; set;} = [];
}