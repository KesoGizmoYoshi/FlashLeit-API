namespace FlashLeit_API.Models.B2CRelatedModels;

public class RegistrationClaimsModel
{
    public string? Email { get; set; }
    public string? DisplayName { get; set; } // DispayName from B2C = AccountName in our back-end
}
