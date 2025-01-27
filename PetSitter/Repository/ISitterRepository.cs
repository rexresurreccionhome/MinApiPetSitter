namespace PetSitter.Repository;

using PetSitter.Models;


public interface ISitterRepository {
     public List<Sitter> GetSitters();
     public Sitter? GetSitter(Guid sitterId);
     public Sitter CreateSitter(SitterInput sitterInput);
     public Sitter? UpdateSitter(Guid sitterId, SitterInput sitterInput);
     public void DeleteSitter(Guid sitterId);
}