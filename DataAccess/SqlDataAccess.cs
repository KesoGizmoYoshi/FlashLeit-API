using Microsoft.EntityFrameworkCore.Metadata;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using FlashLeit_API.Services;

namespace FlashLeit_API.DataAccess;

public class SqlDataAccess : ISqlDataAccess
{
    // NuGet packages:
    // Microsoft.Extensions.Configuration
    // Dapper
    // System.Data.SqlClient


    private readonly IConnectionStringService _connection;

    public SqlDataAccess(IConnectionStringService connection)
    {
        _connection = connection;
    }

    // Pass in name of the stored procedure, the parameters and the connection string name:
    // LoadData is used for GET and POST actions, because often when we post - we also want to return (load)
    // data to the user.
    public async Task<List<T>> LoadData<T, U>(
        string storedProcedure,
        U parameters)
    {
        string connectionsString = _connection.GetConnectionStringFromAzureKeyVault();


        // IDbConnection open a line of communication to the SQL database using the connections string.
        // "using" statement helps making sure the connection get closed at the end of the code block.
        using IDbConnection connection = new SqlConnection(connectionsString);

        // Query helps mapping the model, in this case to a generic type. Depending
        // on what type we pass in, we get the mapping of a different object.
        var rows = await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

        return rows.ToList();
    }


    public Task SaveData<T>(
        string storedProcedure,
        T parameters)
    {
        string connectionString = _connection.GetConnectionStringFromAzureKeyVault();

        using IDbConnection connection = new SqlConnection(connectionString);

        return connection.ExecuteAsync(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure);
    }


}
