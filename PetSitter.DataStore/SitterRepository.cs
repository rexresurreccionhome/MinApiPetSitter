namespace PetSitter.DataStore;

using System.Diagnostics.CodeAnalysis;
using Dapper;

using PetSitter.Domain.Interface;
using PetSitter.Domain.Models;


[ExcludeFromCodeCoverage]
public class SitterRepository(IDbConnectionFactory dbConnectionFactory) : ISitterRepository
{

    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    private Sitter GetPersistedSitter(Guid sittertId)
    {
        Sitter persistedSitter = _dbConnectionFactory.DbConn().QuerySingle<Sitter>("SELECT * FROM PetSitterSchema.Sitter WHERE SitterId = @SitterId", new { SitterId = sittertId });
        return persistedSitter;
    }

    public List<Sitter> GetSitters()
    {
        List<Sitter> sitters = [.. _dbConnectionFactory.DbConn().Query<Sitter>("SELECT * FROM PetSitterSchema.Sitter;")];
        return sitters;
    }

    public Sitter? GetSitter(Guid sitterId)
    {
        Sitter? sitter = _dbConnectionFactory.DbConn().QueryFirstOrDefault<Sitter>("SELECT * FROM PetSitterSchema.Sitter WHERE PetId = @SitterId", new { SitterId = sitterId });
        return sitter;
    }

    public Sitter CreateSitter(SitterBase sitterInput)
    {
        Sitter insertSitter = new() { Name = sitterInput.Name };
        _dbConnectionFactory.DbConn().Execute("INSERT INTO PetSitterSchema.Sitter(SittertId, Name) VALUES (@SitterId, @Name)", insertSitter);
        return GetPersistedSitter(insertSitter.SitterId);
    }

    public Sitter? UpdateSitter(Guid sitterId, SitterBase sitterInput)
    {
        _dbConnectionFactory.DbConn().Execute("UPDATE PetSitterSchema.Sitter SET Name = @Name WHERE SitterId = @SitterId", new { SitterId = sitterId, Name = sitterInput.Name });
        return GetPersistedSitter(sitterId);
    }

    public void DeleteSitter(Guid sitterId)
    {
        _dbConnectionFactory.DbConn().Execute("DELETE FROM PetSitterSchema.Sitter WHERE PetId = @SitterId", new { SitterId = sitterId });
    }
}

