using System.Text.Json.Serialization;

namespace TAF.Business.ApiModels;

public sealed class Geo
{
    [JsonPropertyName("lat")]
    public string? Lat { get; set; }

    [JsonPropertyName("lng")]
    public string? Lng { get; set; }
}
