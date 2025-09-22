using LeadManager.Application.Leads.DTOs;
using MediatR;

namespace LeadManager.Application.Leads.Queries;

public class GetInvitedLeadsQuery : IRequest<List<LeadInvitedDto>>
{
}

