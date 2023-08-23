using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace FlashLeit_API.Services;

public class ConnectionStringService : IConnectionStringService
{
    public string GetConnectionStringFromAzureKeyVault()
    {
        SecretClientOptions options = new SecretClientOptions()
        {
            Retry =
            {
                Delay= TimeSpan.FromSeconds(2),
                MaxDelay = TimeSpan.FromSeconds(16),
                MaxRetries = 5,
                Mode = RetryMode.Exponential
            }
        };

        var client = new SecretClient(new Uri("https://flashleit-keys.vault.azure.net/"), new DefaultAzureCredential(), options);

        KeyVaultSecret secret = client.GetSecret("ConnectionString--FlashleitDbConnection");

        return secret.Value;
    }
}
