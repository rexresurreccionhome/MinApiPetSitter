namespace PetSitter.DB;

using PetSitter.Models;


// This Class will implement an InMemory Store to replicate database operations for Sitters
public static class SitterDB {
    public static List<Sitter> Sitters{get; set;} = [
        new Sitter{Name="Jamie"},
        new Sitter{Name="John"},
        new Sitter{Name="Hans"},
        new Sitter{Name="Jane"},
    ];
}