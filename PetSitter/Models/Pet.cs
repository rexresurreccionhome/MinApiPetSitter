
namespace PetSitter.Models;


public abstract class PetBase {
    public required string Name {get; set;}
}

public class Pet: PetBase {

    public Guid PetId {get; set;} = Guid.NewGuid();
    
}

public class PetInput: PetBase;

public class GetPetsResponse {
    public required List<Pet> Pets {get; set;} = [];
}