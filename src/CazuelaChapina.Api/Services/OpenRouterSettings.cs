public class OpenRouterSettings
{
    public string ApiKey { get; set; } = Environment.GetEnvironmentVariable("OPEN_KEY") ?? string.Empty;
    public string ModelId { get; set; } = Environment.GetEnvironmentVariable("ModelId") ?? "openrouter:deepseek/deepseek-r1:free";
}
