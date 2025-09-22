namespace LeadManager.Application.Leads.DTOs;

public class LeadInvitedDto
{
    public int Id { get; set; }
    public string ContactFirstName { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public string Suburb { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

