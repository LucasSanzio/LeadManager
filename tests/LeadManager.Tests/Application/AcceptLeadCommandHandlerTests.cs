using FluentAssertions;
using LeadManager.Application.Common.Interfaces;
using LeadManager.Application.Leads.Commands;
using LeadManager.Application.Leads.Interfaces;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using Moq;

namespace LeadManager.Tests.Application;

public class AcceptLeadCommandHandlerTests
{
    private readonly Mock<ILeadRepository> _mockLeadRepository;
    private readonly Mock<INotificationService> _mockNotificationService;
    private readonly AcceptLeadCommandHandler _handler;

    public AcceptLeadCommandHandlerTests()
    {
        _mockLeadRepository = new Mock<ILeadRepository>();
        _mockNotificationService = new Mock<INotificationService>();
        _handler = new AcceptLeadCommandHandler(_mockLeadRepository.Object, _mockNotificationService.Object);
    }

    [Fact]
    public async Task Handle_WhenLeadExists_ShouldAcceptLeadAndSendNotification()
    {
        // Arrange
        var leadId = 1;
        var lead = new Lead
        {
            Id = leadId,
            Status = LeadStatus.Invited,
            Price = 600,
            ContactFirstName = "John",
            ContactLastName = "Doe",
            ContactEmail = "john.doe@example.com",
            Category = "Plumbing",
            Suburb = "Downtown"
        };

        _mockLeadRepository.Setup(x => x.GetByIdAsync(leadId))
            .ReturnsAsync(lead);

        var command = new AcceptLeadCommand { LeadId = leadId };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        lead.Status.Should().Be(LeadStatus.Accepted);
        lead.Price.Should().Be(540); // 10% discount applied

        _mockLeadRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        _mockNotificationService.Verify(x => x.SendNotificationAsync(
            lead.ContactEmail,
            "Lead Accepted!",
            It.Is<string>(body => body.Contains("John") && body.Contains("$540.00"))),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WhenLeadDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var leadId = 999;
        _mockLeadRepository.Setup(x => x.GetByIdAsync(leadId))
            .ReturnsAsync((Lead?)null);

        var command = new AcceptLeadCommand { LeadId = leadId };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
        _mockLeadRepository.Verify(x => x.SaveChangesAsync(), Times.Never);
        _mockNotificationService.Verify(x => x.SendNotificationAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenLeadPriceIs500OrLess_ShouldNotApplyDiscount()
    {
        // Arrange
        var leadId = 1;
        var lead = new Lead
        {
            Id = leadId,
            Status = LeadStatus.Invited,
            Price = 400,
            ContactFirstName = "Jane",
            ContactLastName = "Smith",
            ContactEmail = "jane.smith@example.com",
            Category = "Electrical",
            Suburb = "Uptown"
        };

        _mockLeadRepository.Setup(x => x.GetByIdAsync(leadId))
            .ReturnsAsync(lead);

        var command = new AcceptLeadCommand { LeadId = leadId };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        lead.Status.Should().Be(LeadStatus.Accepted);
        lead.Price.Should().Be(400); // No discount applied

        _mockNotificationService.Verify(x => x.SendNotificationAsync(
            lead.ContactEmail,
            "Lead Accepted!",
            It.Is<string>(body => body.Contains("Jane") && body.Contains("$400.00"))),
            Times.Once);
    }
}

