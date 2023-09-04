namespace FlashLeit_API.Services;

public class PublicKeyService : IPublicKeyService
{
    public PublicKeyService()
    {

    }


    public int ConstructPublicKey(int userId, int itemId)
    {
        string concatenatedNumberString = $"{userId}{itemId}";

        int result = int.Parse(concatenatedNumberString);

        return result;
    }
}
