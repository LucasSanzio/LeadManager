using LeadManager.Application.Leads.DTOs;
using MediatR;

namespace LeadManager.Application.Leads.Queries;

public class GetAcceptedLeadsQuery : IRequest<List<LeadAcceptedDto>>
{
}

