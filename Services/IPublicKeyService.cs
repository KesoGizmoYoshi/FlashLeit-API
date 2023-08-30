namespace FlashLeit_API.Services;

public interface IPublicKeyService
{
    int ConstructPublicKey(int userId, int collectionId);
}