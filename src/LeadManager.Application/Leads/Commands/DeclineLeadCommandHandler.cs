using LeadManager.Application.Leads.Interfaces;
using MediatR;

namespace LeadManager.Application.Leads.Commands;

public class DeclineLeadCommandHandler : IRequestHandler<DeclineLeadCommand, bool>
{
    private readonly ILeadRepository _leadRepository;

    public DeclineLeadCommandHandler(ILeadRepository leadRepository)
    {
        _leadRepository = leadRepository;
    }

    public async Task<bool> Handle(DeclineLeadCommand request, CancellationToken cancellationToken)
    {
        var lead = await _leadRepository.GetByIdAsync(request.LeadId);

        if (lead == null)
        {
            return false;
        }

        lead.Decline();

        await _leadRepository.SaveChangesAsync();

        return true;
    }
}

