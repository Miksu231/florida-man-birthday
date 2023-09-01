using Newtonsoft.Json;

namespace FloridaMan;

public record Metatags
{
    [JsonProperty("og:image")]
    public string? Image { get; init; }
    [JsonProperty("og:title")]
    public string? Title { get; init; }
    [JsonProperty("og:description")]
    public string? Description { get; init; }
    
}