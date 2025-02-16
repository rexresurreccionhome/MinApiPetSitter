namespace PetSitter.DataStore.Tests;

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Xunit;

using PetSitter.DataStore;
using PetSitter.DataStore.Models;


public class DbConnectionFactoryTests
{
    [Fact]
    public void DbConn_WithConnectionString_ShouldHaveSqlConnection()
    {
        // Arrange
        IOptions<ConnectionStringSettings> connectionStringSettings = Options.Create<ConnectionStringSettings>(new ConnectionStringSettings() { PetSitter = "Server=Test" });
        DbConnectionFactory dbConnectionFactory = new(connectionStringSettings);
        // Act
        IDbConnection dbConnection = dbConnectionFactory.DbConn;
        // Assert
        Assert.IsType<SqlConnection>(dbConnection);
        Assert.Equal("Server=Test", dbConnection.ConnectionString);
    }
}