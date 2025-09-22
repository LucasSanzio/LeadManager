using FluentAssertions;
using LeadManager.Application.Leads.Interfaces;
using LeadManager.Application.Leads.Queries;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using Moq;

namespace LeadManager.Tests.Application;

public class GetInvitedLeadsQueryHandlerTests
{
    private readonly Mock<ILeadRepository> _mockLeadRepository;
    private readonly GetInvitedLeadsQueryHandler _handler;

    public GetInvitedLeadsQueryHandlerTests()
    {
        _mockLeadRepository = new Mock<ILeadRepository>();
        _handler = new GetInvitedLeadsQueryHandler(_mockLeadRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnInvitedLeadsAsDto()
    {
        // Arrange
        var invitedLeads = new List<Lead>
        {
            new Lead
            {
                Id = 1,
                ContactFirstName = "John",
                ContactLastName = "Doe",
                Status = LeadStatus.Invited,
                Price = 500,
                Category = "Plumbing",
                Suburb = "Downtown",
                Description = "Fix kitchen sink",
                DateCreated = DateTime.UtcNow.AddDays(-1)
            },
            new Lead
            {
                Id = 2,
                ContactFirstName = "Jane",
                ContactLastName = "Smith",
                Status = LeadStatus.Invited,
                Price = 750,
                Category = "Electrical",
                Suburb = "Uptown",
                Description = "Install new outlets",
                DateCreated = DateTime.UtcNow.AddDays(-2)
            }
        };

        _mockLeadRepository.Setup(x => x.GetInvitedLeadsAsync())
            .ReturnsAsync(invitedLeads);

        var query = new GetInvitedLeadsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
        
        var firstLead = result.First();
        firstLead.Id.Should().Be(1);
        firstLead.ContactFirstName.Should().Be("John");
        firstLead.Price.Should().Be(500);
        firstLead.Category.Should().Be("Plumbing");
        firstLead.Suburb.Should().Be("Downtown");
        firstLead.Description.Should().Be("Fix kitchen sink");

        var secondLead = result.Skip(1).First();
        secondLead.Id.Should().Be(2);
        secondLead.ContactFirstName.Should().Be("Jane");
        secondLead.Price.Should().Be(750);
        secondLead.Category.Should().Be("Electrical");
        secondLead.Suburb.Should().Be("Uptown");
        secondLead.Description.Should().Be("Install new outlets");
    }

    [Fact]
    public async Task Handle_WhenNoInvitedLeads_ShouldReturnEmptyList()
    {
        // Arrange
        _mockLeadRepository.Setup(x => x.GetInvitedLeadsAsync())
            .ReturnsAsync(new List<Lead>());

        var query = new GetInvitedLeadsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
}

