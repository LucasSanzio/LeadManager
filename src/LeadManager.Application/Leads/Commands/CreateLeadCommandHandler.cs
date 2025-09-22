using System;
using LeadManager.Application.Leads.DTOs;
using LeadManager.Application.Leads.Interfaces;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using MediatR;

namespace LeadManager.Application.Leads.Commands;

public class CreateLeadCommandHandler : IRequestHandler<CreateLeadCommand, LeadInvitedDto>
{
    private readonly ILeadRepository _leadRepository;

    public CreateLeadCommandHandler(ILeadRepository leadRepository)
    {
        _leadRepository = leadRepository;
    }

    public async Task<LeadInvitedDto> Handle(CreateLeadCommand request, CancellationToken cancellationToken)
    {
        var lead = new Lead
        {
            ContactFirstName = request.ContactFirstName,
            ContactLastName = request.ContactLastName,
            ContactEmail = request.ContactEmail,
            ContactPhone = request.ContactPhone,
            Suburb = request.Suburb,
            Category = request.Category,
            Description = request.Description,
            Price = request.Price,
            Status = LeadStatus.Invited,
            DateCreated = DateTime.UtcNow
        };

        var createdLead = await _leadRepository.AddAsync(lead);

        return new LeadInvitedDto
        {
            Id = createdLead.Id,
            ContactFirstName = createdLead.ContactFirstName,
            DateCreated = createdLead.DateCreated,
            Suburb = createdLead.Suburb,
            Category = createdLead.Category,
            Description = createdLead.Description,
            Price = createdLead.Price
        };
    }
}
