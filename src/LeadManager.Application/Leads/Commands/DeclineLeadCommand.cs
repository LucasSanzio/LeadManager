using MediatR;

namespace LeadManager.Application.Leads.Commands;

public class DeclineLeadCommand : IRequest<bool>
{
    public int LeadId { get; set; }
}

