namespace Reado.Api;

public static class ApiConfiguration
{
    public const string CorsPolicyName = "wasm";
    public static string StripeApiKey { get; set; } = string.Empty;
}
public class OpenAIOptions
{
    public string ApiKey { get; set; } = string.Empty;
}
