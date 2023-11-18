using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FloridaMan.Models;

public record DateDto
{
    [Required]
    [JsonProperty("day")]
    public string Day { get; init; } = string.Empty;
    [Required]
    [JsonProperty("month")]
    public string Month { get; init; } = string.Empty;
}