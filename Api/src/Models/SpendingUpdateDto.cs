// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace WebApi.src.Models
{
  public class SpendingUpdateDto
  {
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("costCategoryId")]
    public int CostCategoryId { get; set; }
  }
}
