namespace FlashLeit_API.Models.B2CRelatedModels;

public class SuccessfulValidationModel
{
    public string? Version { get; set; } = "1.0.0";
    public string? Action { get; set; } = "Continue";
    public string? Extension_UserId { get; set; } = null;
}
