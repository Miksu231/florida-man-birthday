using Newtonsoft.Json;

namespace FloridaMan;

public record DisplayResult
{
    public string? Title { get; init; } = string.Empty;
    public string Link { get; init; } = string.Empty;
    public string? Snippet { get; init; } = string.Empty;

    public string? ImageLink { get; init;}
}
