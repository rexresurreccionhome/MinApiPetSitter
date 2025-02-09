
namespace PetSitter.Domain.Models;


public abstract class SitterBase
{
    public required string Name { get; set; }
}

public class Sitter : SitterBase
{

    public Guid SitterId { get; set; } = Guid.NewGuid();

}
