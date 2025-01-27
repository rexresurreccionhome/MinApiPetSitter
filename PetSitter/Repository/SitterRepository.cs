namespace PetSitter.Repository;

using PetSitter.Models;
using PetSitter.DB;


public class SitterRepository: ISitterRepository {

    public SitterRepository(List<Sitter> sitters) {
        SitterDB.Sitters = sitters;
    }

    public List<Sitter> GetSitters() => SitterDB.Sitters;

    public Sitter? GetSitter(Guid sitterId) => SitterDB.Sitters.Find(sitter => sitter.SitterId == sitterId);

    public Sitter CreateSitter(SitterInput sitterInput) {
        SitterDB.Sitters.Add(new Sitter{Name=sitterInput.Name});
        return SitterDB.Sitters.Last();
    }

    public Sitter? UpdateSitter(Guid sitterId, SitterInput sitterInput) {
        foreach(Sitter sitter in SitterDB.Sitters) {
            if(sitter.SitterId == sitterId) {
                sitter.Name = sitterInput.Name;
            }
        }
        return SitterDB.Sitters.Find(sitter => sitter.SitterId == sitterId);
    }

    public void DeleteSitter(Guid sitterId) {
        foreach(Sitter sitter in SitterDB.Sitters.ToList()) {
            if(sitter.SitterId == sitterId) {
                SitterDB.Sitters.Remove(sitter);
            }
        }
    }
}

