namespace FlashLeit_API.Models.B2CRelatedModels;

public class FailedValidationModel
{
    public string? Version { get; set; } = "1.0.0";
    public string? Status { get; set; } = "400";
    public string? Action { get; set; } = "ValidationError";
    public string? UserMessage { get; set; } = "Username is already in use!"; 
}
