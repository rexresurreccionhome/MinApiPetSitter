namespace PetSitter.Domain.Interface;

using PetSitter.Domain.Models;


public interface ISitterRepository {
     public List<Sitter> GetSitters();
     public Sitter? GetSitter(Guid sitterId);
     public Sitter CreateSitter(SitterBase sitterInput);
     public Sitter? UpdateSitter(Guid sitterId, SitterBase sitterInput);
     public void DeleteSitter(Guid sitterId);
}