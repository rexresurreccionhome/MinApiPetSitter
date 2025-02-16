using System.Data;

namespace PetSitter.DataStore;


public interface IDbConnectionFactory
{
    public IDbConnection DbConn{get;}
}