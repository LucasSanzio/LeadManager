using FluentAssertions;
using LeadManager.Application.Leads.Commands;
using LeadManager.Application.Leads.Interfaces;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using Moq;

namespace LeadManager.Tests.Application;

public class DeclineLeadCommandHandlerTests
{
    private readonly Mock<ILeadRepository> _mockLeadRepository;
    private readonly DeclineLeadCommandHandler _handler;

    public DeclineLeadCommandHandlerTests()
    {
        _mockLeadRepository = new Mock<ILeadRepository>();
        _handler = new DeclineLeadCommandHandler(_mockLeadRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenLeadExists_ShouldDeclineLead()
    {
        // Arrange
        var leadId = 1;
        var lead = new Lead
        {
            Id = leadId,
            Status = LeadStatus.Invited,
            Price = 600,
            ContactFirstName = "John",
            ContactLastName = "Doe"
        };

        _mockLeadRepository.Setup(x => x.GetByIdAsync(leadId))
            .ReturnsAsync(lead);

        var command = new DeclineLeadCommand { LeadId = leadId };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        lead.Status.Should().Be(LeadStatus.Declined);
        _mockLeadRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenLeadDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var leadId = 999;
        _mockLeadRepository.Setup(x => x.GetByIdAsync(leadId))
            .ReturnsAsync((Lead?)null);

        var command = new DeclineLeadCommand { LeadId = leadId };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
        _mockLeadRepository.Verify(x => x.SaveChangesAsync(), Times.Never);
    }
}

