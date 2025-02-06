namespace PetSitter.DataStore;

using System.Diagnostics.CodeAnalysis;
using Dapper;

using PetSitter.Domain.Interface;
using PetSitter.Domain.Models;


[ExcludeFromCodeCoverage]
public class PetRepository(IDbConnectionFactory dbConnectionFactory) : IPetRepository {
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    private Pet GetPersistedPet(Guid petId) {
        Pet insertedPet = _dbConnectionFactory.DbConn().QuerySingle<Pet>("SELECT * FROM PetSitterSchema.Pet WHERE PetId = @PetId", new {PetId=petId});
        return insertedPet;
    }

    public List<Pet> GetPets() {
        List<Pet> pets = [.. _dbConnectionFactory.DbConn().Query<Pet>("SELECT * FROM PetSitterSchema.Pet;")];
        return pets;
    }

    public Pet? GetPet(Guid petId) {
        Pet? pet = _dbConnectionFactory.DbConn().QueryFirstOrDefault<Pet>("SELECT * FROM PetSitterSchema.Pet WHERE PetId = @PetId", new {PetId=petId});
        return pet;
    }

    public Pet CreatePet(PetBase petInput) {
        Pet insertPet = new() { Name=petInput.Name };
        _dbConnectionFactory.DbConn().Execute("INSERT INTO PetSitterSchema.Pet(PetId, Name) VALUES (@PetId, @Name)", insertPet);
        return GetPersistedPet(insertPet.PetId);
    }

    public Pet? UpdatePet(Guid petId, PetBase petInput) {
        _dbConnectionFactory.DbConn().Execute("UPDATE PetSitterSchema.Pet SET Name = @Name WHERE PetId = @PetId", new {PetId=petId, Name=petInput.Name});
        return GetPersistedPet(petId);
    }

    public void DeletePet(Guid petId) {
        _dbConnectionFactory.DbConn().Execute("DELETE FROM PetSitterSchema.Pet WHERE PetId = @PetId", new {PetId=petId});
    }
}