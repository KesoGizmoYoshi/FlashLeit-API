using Dapper;
using System.Data;
using System.Data.SqlClient;
using FlashLeit_API.Services;
using System.Diagnostics;
using flashleit_class_library.Models;

namespace FlashLeit_API.DataAccess;

// This class talks to SQL through Dapper

public class SqlDataAccess : ISqlDataAccess
{
    // NuGet packages:
    // Microsoft.Extensions.Configuration --> Allows us to talk to appsettings.json
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


    public async Task<int> SaveData<T>(
        string storedProcedure,
        T parameters)
    {
        string connectionString = _connection.GetConnectionStringFromAzureKeyVault();

        using IDbConnection connection = new SqlConnection(connectionString);

        int affectedRows = await connection.ExecuteAsync(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure);

        return affectedRows;
    }

    public async Task<CollectionModel> GetCollectionWithCardsAsync(string storedProcedure, int collectionId)
    {
        string connectionString = _connection.GetConnectionStringFromAzureKeyVault();

        using IDbConnection connection = new SqlConnection(connectionString);

        // Dictionary<TKey, TValue> to store key-value pairs.
        var lookup = new Dictionary<int, CollectionModel>();

        // I expect to map <CollectionModel>, <CardModel> in the return type <CollectionModel>

        connection.Query<CollectionModel, CardModel, CollectionModel>(
            storedProcedure,
            (collection, card) =>
            {
                CollectionModel currentCollection;

                // If dictionary doesn't contain the data from the database, it will add
                // the CollectionId (key) and CollectionModel (value) in the form of the
                // data from the database.

                if (!lookup.TryGetValue(collection.Id, out currentCollection))
                {
                    lookup.Add(collection.Id, currentCollection = collection);
                }

                currentCollection.FlashCards.Add(card);
                return currentCollection;
            },
            new {Id = collectionId},
            splitOn: "Id",
            commandType: CommandType.StoredProcedure).AsQueryable();

        var resultsList = lookup.Values;

        return resultsList.FirstOrDefault();
    }

    public async Task<List<CollectionModel>> GetCollectionsByUserId(string storedProcedure, int userId)
    {
        string connectionString = _connection.GetConnectionStringFromAzureKeyVault();

        using IDbConnection connection = new SqlConnection(connectionString);

        var lookup = new Dictionary<int, UserModel>();

        connection.Query<UserModel, CollectionModel, UserModel>(
            storedProcedure,
            (user, collection) =>
            {
                UserModel currentUser;

                if (!lookup.TryGetValue(user.Id, out currentUser))
                {
                    lookup.Add(user.Id, currentUser = user);
                }

                currentUser.Collections.Add(collection);
                return currentUser;
            },
            new { UserId = userId },
            splitOn: "CollectionId",
            commandType: CommandType.StoredProcedure).AsQueryable();

        UserModel results = lookup.Values.FirstOrDefault();

        return results.Collections;
    }
}
