// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.src.Models
{
  [Table("Spending")]
  public class Spending
  {
    [Key]
    public int Id { get; set; }

    [MaxLength(150)]
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }

    public int CostCategoryId { get; set; }

    [JsonIgnore]
    public CostCategory? CostCategory { get; set; }
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    [JsonIgnore]
    public User? User { get; set; }
  }
}
