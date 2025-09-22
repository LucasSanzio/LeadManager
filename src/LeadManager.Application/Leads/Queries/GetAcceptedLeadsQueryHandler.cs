using LeadManager.Application.Leads.DTOs;
using LeadManager.Application.Leads.Interfaces;
using LeadManager.Domain.Enums;
using MediatR;

namespace LeadManager.Application.Leads.Queries;

public class GetAcceptedLeadsQueryHandler : IRequestHandler<GetAcceptedLeadsQuery, List<LeadAcceptedDto>>
{
    private readonly ILeadRepository _leadRepository;

    public GetAcceptedLeadsQueryHandler(ILeadRepository leadRepository)
    {
        _leadRepository = leadRepository;
    }

    public async Task<List<LeadAcceptedDto>> Handle(GetAcceptedLeadsQuery request, CancellationToken cancellationToken)
    {
        var acceptedLeads = await _leadRepository.GetByStatusAsync(LeadStatus.Accepted);

        return acceptedLeads.Select(lead => new LeadAcceptedDto
        {
            Id = lead.Id,
            ContactFirstName = lead.ContactFirstName,
            ContactLastName = lead.ContactLastName,
            ContactEmail = lead.ContactEmail,
            ContactPhone = lead.ContactPhone,
            ContactFullName = lead.ContactFullName,
            DateCreated = lead.DateCreated,
            Suburb = lead.Suburb,
            Category = lead.Category,
            Description = lead.Description,
            Price = lead.Price,
            Status = lead.Status.ToString()
        }).ToList();
    }
}

