namespace PetSitter.DataStore;

using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;


public class Program {
    public static void Main () {
        
        IDbConnection dbConnection = new SqlConnection("Server=localhost");
        DateTime now = dbConnection.QuerySingle<DateTime>("SELECT GETDATE()");
        Console.WriteLine(now);
    }
}