using MediatR;

namespace LeadManager.Application.Leads.Commands;

public class AcceptLeadCommand : IRequest<bool>
{
    public int LeadId { get; set; }
}

