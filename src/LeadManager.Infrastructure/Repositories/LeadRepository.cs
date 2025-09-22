using LeadManager.Application.Leads.Interfaces;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using LeadManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeadManager.Infrastructure.Repositories;

public class LeadRepository : ILeadRepository
{
    private readonly AppDbContext _context;

    public LeadRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Lead>> GetInvitedLeadsAsync()
    {
        return await _context.Leads
            .Where(l => l.Status == LeadStatus.Invited)
            .ToListAsync();
    }

    public async Task<List<Lead>> GetAcceptedLeadsAsync()
    {
        return await _context.Leads
            .Where(l => l.Status == LeadStatus.Accepted)
            .ToListAsync();
    }

    public async Task<List<Lead>> GetByStatusAsync(LeadStatus status)
    {
        return await _context.Leads
            .Where(l => l.Status == status)
            .ToListAsync();
    }

    public async Task<Lead?> GetByIdAsync(int id)
    {
        return await _context.Leads
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<Lead> AddAsync(Lead lead)
    {
        await _context.Leads.AddAsync(lead);
        await _context.SaveChangesAsync();
        return lead;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

