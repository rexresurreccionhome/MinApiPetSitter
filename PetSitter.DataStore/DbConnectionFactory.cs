namespace PetSitter.DataStore;

using System.Data;
using Microsoft.Data.SqlClient;



public class DbConnectionFactory(string connectionString) : IDbConnectionFactory {
    private readonly string _connectionString = connectionString;

    public IDbConnection DbConn() {
        return new SqlConnection(_connectionString);
    }
}