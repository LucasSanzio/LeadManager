using LeadManager.Application.Leads.Commands;
using LeadManager.Application.Leads.Queries;
using LeadManager.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LeadManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeadsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeadsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("invited")]
    public async Task<IActionResult> GetInvitedLeads()
    {
        var query = new GetInvitedLeadsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("accepted")]
    public async Task<IActionResult> GetAcceptedLeads()
    {
        var query = new GetAcceptedLeadsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLead([FromBody] CreateLeadRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var command = new CreateLeadCommand
        {
            ContactFirstName = request.ContactFirstName,
            ContactLastName = request.ContactLastName,
            ContactEmail = request.ContactEmail,
            ContactPhone = request.ContactPhone,
            Suburb = request.Suburb,
            Category = request.Category,
            Description = request.Description,
            Price = request.Price
        };

        var createdLead = await _mediator.Send(command);

        return Created($"api/leads/{createdLead.Id}", createdLead);
    }

    [HttpPut("{id}/accept")]
    public async Task<IActionResult> AcceptLead(int id)
    {
        var command = new AcceptLeadCommand { LeadId = id };
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPut("{id}/decline")]
    public async Task<IActionResult> DeclineLead(int id)
    {
        var command = new DeclineLeadCommand { LeadId = id };
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}

