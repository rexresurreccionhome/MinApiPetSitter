namespace PetSitter.DataStore.Tests;

using System.Data;
using Microsoft.Data.SqlClient;
using PetSitter.DataStore;
using Xunit;


public class DbConnectionFactoryTests {
    [Fact]
    public void DbConn_WithConnectionString_ShouldHaveSqlConnection() {
        // Arrange
        DbConnectionFactory dbConnectionFactory = new("Server=Test");
        // Act
        IDbConnection dbConnection = dbConnectionFactory.DbConn();
        // Assert
        Assert.IsType<SqlConnection>(dbConnection);
        Assert.Equal("Server=Test", dbConnection.ConnectionString);
    }
}