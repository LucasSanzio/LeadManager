using System.ComponentModel.DataAnnotations;

namespace LeadManager.API.Models;

public class CreateLeadRequest
{
    [Required]
    [MaxLength(100)]
    public string ContactFirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string ContactLastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string ContactEmail { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string ContactPhone { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Suburb { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Category { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
}
