using LeadManager.Application.Common.Interfaces;
using LeadManager.Application.Leads.Interfaces;
using MediatR;
using System.Globalization;

namespace LeadManager.Application.Leads.Commands;

public class AcceptLeadCommandHandler : IRequestHandler<AcceptLeadCommand, bool>
{
    private readonly ILeadRepository _leadRepository;
    private readonly INotificationService _notificationService;

    public AcceptLeadCommandHandler(ILeadRepository leadRepository, INotificationService notificationService)
    {
        _leadRepository = leadRepository;
        _notificationService = notificationService;
    }

    public async Task<bool> Handle(AcceptLeadCommand request, CancellationToken cancellationToken)
    {
        var lead = await _leadRepository.GetByIdAsync(request.LeadId);

        if (lead == null)
        {
            return false;
        }

        lead.Accept();

        await _leadRepository.SaveChangesAsync();

        // Send notification
        var subject = "Lead Accepted!";
        var culture = CultureInfo.GetCultureInfo("en-US");
        var body = string.Format(
            culture,
            "Dear {0},\n\nYour lead for {1} in {2} has been accepted. The final price is {3}.\n\nRegards,\nLead Management Team",
            lead.ContactFirstName,
            lead.Category,
            lead.Suburb,
            lead.Price.ToString("C", culture));
        await _notificationService.SendNotificationAsync(lead.ContactEmail, subject, body);

        return true;
    }
}
