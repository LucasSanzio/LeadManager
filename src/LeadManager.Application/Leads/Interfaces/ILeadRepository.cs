using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;

namespace LeadManager.Application.Leads.Interfaces;

public interface ILeadRepository
{
    Task<List<Lead>> GetInvitedLeadsAsync();
    Task<List<Lead>> GetAcceptedLeadsAsync();
    Task<List<Lead>> GetByStatusAsync(LeadStatus status);
    Task<Lead?> GetByIdAsync(int id);
    Task<Lead> AddAsync(Lead lead);
    Task SaveChangesAsync();
}

