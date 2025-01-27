
namespace PetSitter.Models;


public abstract class SitterBase {
    public required string Name {get; set;}
}

public class Sitter: SitterBase {

    public Guid SitterId {get; set;} = Guid.NewGuid();
    
}

public class SitterInput: SitterBase;

public class GetSittersResponse {
    public required List<Sitter> Sitters {get; set;} = [];
}