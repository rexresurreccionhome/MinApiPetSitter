namespace PetSitter.DataStore;

using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

using PetSitter.Domain.Models;


public class Program {
    public static void Main () {
        
        IDbConnection dbConnection = new SqlConnection("Server=localhost");
        DateTime now = dbConnection.QuerySingle<DateTime>("SELECT GETDATE()");
        Console.WriteLine(now);
    
        Pet newPet = new() {PetId=new Guid("6f4b7962-1b3a-40b2-9984-702502a7ee30"), Name="Bill"};
        int rowsAffected = dbConnection.Execute("INSERT INTO PetSitterSchema.Pet(PetId, Name) VALUES (@PetId, @Name)", newPet);
        Console.WriteLine($"Rows Affected: {rowsAffected}");

        // Pet pet = dbConnection.QuerySingle<Pet>("SELECT * FROM PetSitterSchema.Pet WHERE PetId = @PetId", new {PetId=newPet.PetId});
        // Console.WriteLine($"{pet.PetId}: {pet.Name}");
        
        Pet? pet = dbConnection.QueryFirstOrDefault<Pet>("SELECT * FROM PetSitterSchema.Pet WHERE PetId = @PetId", new {PetId=new Guid("44c0e7c9-5ea2-4afb-8c72-1e0b74bf59bc")});
        Console.WriteLine(pet == null);

        // Console.WriteLine($"{pet.PetId}: {pet.Name}");

        // List<Pet> pets = [.. dbConnection.Query<Pet>("SELECT * FROM PetSitterSchema.Pet")];
        // foreach(Pet pet in pets) {
        //     Console.WriteLine($"{pet.PetId}: {pet.Name}");
        // }
    }
}