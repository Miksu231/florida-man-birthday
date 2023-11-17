using Newtonsoft.Json;

namespace FloridaMan.Models;

public record DateDto
{
    [JsonProperty("day")]
    public string Day { get; init; } = string.Empty;
    [JsonProperty("month")]
    public string Month { get; init; } = string.Empty;
}