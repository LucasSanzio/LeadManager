using System;
using FluentAssertions;
using LeadManager.Application.Leads.Commands;
using LeadManager.Application.Leads.Interfaces;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using Moq;

namespace LeadManager.Tests.Application;

public class CreateLeadCommandHandlerTests
{
    private readonly Mock<ILeadRepository> _leadRepositoryMock = new();
    private readonly CreateLeadCommandHandler _handler;

    public CreateLeadCommandHandlerTests()
    {
        _handler = new CreateLeadCommandHandler(_leadRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldPersistLeadAndReturnDto()
    {
        // Arrange
        var command = new CreateLeadCommand
        {
            ContactFirstName = "Jane",
            ContactLastName = "Doe",
            ContactEmail = "jane.doe@example.com",
            ContactPhone = "+1 555 0101",
            Suburb = "Sydney",
            Category = "Plumbing",
            Description = "Fix leaking tap",
            Price = 320.75m
        };

        Lead? capturedLead = null;

        _leadRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Lead>()))
            .ReturnsAsync((Lead lead) =>
            {
                capturedLead = lead;
                lead.Id = 42;
                return lead;
            });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _leadRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Lead>()), Times.Once);
        result.Id.Should().Be(42);
        result.ContactFirstName.Should().Be(command.ContactFirstName);
        result.Category.Should().Be(command.Category);
        result.Description.Should().Be(command.Description);
        result.Price.Should().Be(command.Price);

        capturedLead.Should().NotBeNull();
        capturedLead!.ContactEmail.Should().Be(command.ContactEmail);
        capturedLead!.Status.Should().Be(LeadStatus.Invited);
        capturedLead!.DateCreated.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }
}
