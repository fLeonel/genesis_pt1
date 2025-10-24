using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class LlmService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _modelId;

    public LlmService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiKey = Environment.GetEnvironmentVariable("OPEN_KEY") ?? throw new Exception("OPEN_KEY missing");
        _modelId = Environment.GetEnvironmentVariable("Model_Id") ?? "openai/gpt-4o-mini";
    }

    public async Task<string> AskAsync(string prompt)
    {
        var payload = new
        {
            model = _modelId,
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);

        var response = await _httpClient.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
        var body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"LLM error: {response.StatusCode}");
            Console.WriteLine(body);
            throw new Exception(body);
        }

        var doc = JsonDocument.Parse(body);
        return doc.RootElement
                  .GetProperty("choices")[0]
                  .GetProperty("message")
                  .GetProperty("content")
                  .GetString() ?? "";
    }
}
