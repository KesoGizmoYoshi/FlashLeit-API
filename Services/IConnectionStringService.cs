namespace FlashLeit_API.Services;

public interface IConnectionStringService
{
    public string GetConnectionStringFromAzureKeyVault();
}
