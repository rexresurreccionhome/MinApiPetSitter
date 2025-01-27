namespace PetSitter.DB;

using PetSitter.Models;


// This Class will implement an InMemory Store to replicate database operations for Pets
public static class PetDB {
    public static List<Pet> Pets{get; set;} = [
        new Pet{Name="Lola"},
        new Pet{Name="Max"},
        new Pet{Name="Sadie"},
        new Pet{Name="Cooper"},
        new Pet{Name="Bear"},
        new Pet{Name="Loki"},
        new Pet{Name="Daisy"},
        new Pet{Name="Bella"},
        new Pet{Name="Charlie"},
        new Pet{Name="Luna"},
        new Pet{Name="Leo"}
    ];
}