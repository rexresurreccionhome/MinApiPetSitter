
namespace PetSitter.Models;

using PetSitter.Domain.Models;


public class SitterInput: SitterBase;

public class GetSittersResponse {
    public required List<Sitter> Sitters {get; set;} = [];
}