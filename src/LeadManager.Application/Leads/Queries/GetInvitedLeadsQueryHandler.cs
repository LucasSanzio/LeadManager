using LeadManager.Application.Leads.DTOs;
using LeadManager.Application.Leads.Interfaces;
using MediatR;

namespace LeadManager.Application.Leads.Queries;

public class GetInvitedLeadsQueryHandler : IRequestHandler<GetInvitedLeadsQuery, List<LeadInvitedDto>>
{
    private readonly ILeadRepository _leadRepository;

    public GetInvitedLeadsQueryHandler(ILeadRepository leadRepository)
    {
        _leadRepository = leadRepository;
    }

    public async Task<List<LeadInvitedDto>> Handle(GetInvitedLeadsQuery request, CancellationToken cancellationToken)
    {
        var invitedLeads = await _leadRepository.GetInvitedLeadsAsync();

        var result = invitedLeads.Select(l => new LeadInvitedDto
        {
            Id = l.Id,
            ContactFirstName = l.ContactFirstName,
            DateCreated = l.DateCreated,
            Suburb = l.Suburb,
            Category = l.Category,
            Description = l.Description,
            Price = l.Price
        }).ToList();

        return result;
    }
}

