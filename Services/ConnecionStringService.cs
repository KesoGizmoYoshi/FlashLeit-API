using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace FlashLeit_API.Services;

public class ConnectionStringService : IConnectionStringService
{
    private readonly SecretClient _client;
    private readonly SecretClientOptions _options;

    // The SecretClient is used for accessing a specified Azure Key Vault.
    // Only options regarding Retry is defined for the SecretClient.
    // Azure Key Vault offers many different ways to authenticate user/application access.
    // The default method is DefaultAzureCredential(), because it tries every available auth method.
    // This is a very slow method to use during development. But really fast, if the application is deployed in Azure.
    // So we opted to use ChainedTokenCredential() which allow us to decide which auth methods that should be used.
    // This also lets us decide in which order the auth methods will be used.
    // So in our case SecretClient will use AzureCliCredential() first, this is by far the fastest auth method for local development.
    // Only requires that the user login with Azure Cli. Command: "az login"
    // And ManagedIdentityCredential() is the auth method that is used when the application is deployed in Azure.
    public ConnectionStringService()
    {
        _options = new SecretClientOptions()
        {
            Retry =
            {
                Delay= TimeSpan.FromSeconds(2),
                MaxDelay = TimeSpan.FromSeconds(16),
                MaxRetries = 5,
                Mode = RetryMode.Exponential
            }
        };

        _client = new SecretClient(new Uri("https://flashleit-keys.vault.azure.net/"), 
            new ChainedTokenCredential(new AzureCliCredential(), new ManagedIdentityCredential()), _options);
    }

    // This method uses the initialized SecretClient to access our Azure Key Vault.
    // By using the GetSecret() method, we can obtain any secrets safely stored in Azure Key Vault.
    // The required parameter for GetSecret() is whatever name you gave it, when the secret was created.
    // Finally the method returns our connection string.
    public string GetConnectionStringFromAzureKeyVault()
    {
        KeyVaultSecret connectionString = _client.GetSecret("ConnectionString--FlashleitDbConnection");

        return connectionString.Value;
    }
}
