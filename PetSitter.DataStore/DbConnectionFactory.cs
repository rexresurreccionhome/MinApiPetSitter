namespace PetSitter.DataStore;

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

using PetSitter.DataStore.Models;


public class DbConnectionFactory(IOptions<ConnectionStringSettings> configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.Value.PetSitter;

    public IDbConnection DbConn{
        get{
            return new SqlConnection(_connectionString);
        }
    }
}