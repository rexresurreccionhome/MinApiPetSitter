
namespace PetSitter.Domain.Models;


public abstract class PetBase
{
    public required string Name { get; set; }
}

public class Pet : PetBase
{
    public Guid PetId { get; set; } = Guid.NewGuid();
}
