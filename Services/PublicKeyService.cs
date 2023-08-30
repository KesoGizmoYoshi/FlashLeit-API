namespace FlashLeit_API.Services;

public class PublicKeyService : IPublicKeyService
{
    public PublicKeyService()
    {

    }


    public int ConstructPublicKey(int userId, int collectionId)
    {
        string concatenatedNumberString = $"{userId}{collectionId}";

        int result = int.Parse(concatenatedNumberString);

        return result;
    }
}
