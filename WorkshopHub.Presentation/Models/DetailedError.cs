﻿using System.Text.Json.Serialization;

namespace WorkshopHub.Presentation.Models
{
    public sealed class DetailedError
    {
        [JsonPropertyName("code")] public string Code { get; init; } = string.Empty;

        [JsonPropertyName("data")] public object? Data { get; init; }
    }
}
