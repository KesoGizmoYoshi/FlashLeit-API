using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace FlashLeit_API.Services;

public class ConnectionStringService : IConnectionStringService
{
    private readonly SecretClient _client;
    private readonly SecretClientOptions _options;

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
    public string GetConnectionStringFromAzureKeyVault()
    {
        KeyVaultSecret connectionString = _client.GetSecret("ConnectionString--FlashleitDbConnection");

        return connectionString.Value;
    }
}
