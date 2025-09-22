using LeadManager.Domain.Enums;

namespace LeadManager.Domain.Entities;

public class Lead
{
    public int Id { get; set; }
    public string ContactFirstName { get; set; } = string.Empty;
    public string ContactLastName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public string Suburb { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public LeadStatus Status { get; set; }

    // Propriedade calculada para nome completo
    public string ContactFullName => $"{ContactFirstName} {ContactLastName}".Trim();

    public Lead()
    {
        Status = LeadStatus.Invited;
        DateCreated = DateTime.UtcNow;
    }

    public void Accept()
    {
        if (Status != LeadStatus.Invited)
            throw new InvalidOperationException("Only invited leads can be accepted.");

        // Aplicar desconto de 10% se o preço for maior que $500
        if (Price > 500)
        {
            Price = Price * 0.90m;
        }

        Status = LeadStatus.Accepted;
    }

    public void Decline()
    {
        if (Status != LeadStatus.Invited)
            throw new InvalidOperationException("Only invited leads can be declined.");

        Status = LeadStatus.Declined;
    }
}

