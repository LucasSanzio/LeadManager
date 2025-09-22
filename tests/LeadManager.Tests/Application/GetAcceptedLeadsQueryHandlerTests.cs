using FluentAssertions;
using LeadManager.Application.Leads.Interfaces;
using LeadManager.Application.Leads.Queries;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using Moq;

namespace LeadManager.Tests.Application;

public class GetAcceptedLeadsQueryHandlerTests
{
    private readonly Mock<ILeadRepository> _mockLeadRepository;
    private readonly GetAcceptedLeadsQueryHandler _handler;

    public GetAcceptedLeadsQueryHandlerTests()
    {
        _mockLeadRepository = new Mock<ILeadRepository>();
        _handler = new GetAcceptedLeadsQueryHandler(_mockLeadRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAcceptedLeadsAsDto()
    {
        // Arrange
        var acceptedLeads = new List<Lead>
        {
            new Lead
            {
                Id = 1,
                ContactFirstName = "John",
                ContactLastName = "Doe",
                ContactEmail = "john.doe@example.com",
                ContactPhone = "+1234567890",
                Status = LeadStatus.Accepted,
                Price = 450, // After 10% discount from 500
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
                ContactEmail = "jane.smith@example.com",
                ContactPhone = "+0987654321",
                Status = LeadStatus.Accepted,
                Price = 675, // After 10% discount from 750
                Category = "Electrical",
                Suburb = "Uptown",
                Description = "Install new outlets",
                DateCreated = DateTime.UtcNow.AddDays(-2)
            }
        };

        _mockLeadRepository.Setup(x => x.GetByStatusAsync(LeadStatus.Accepted))
            .ReturnsAsync(acceptedLeads);

        var query = new GetAcceptedLeadsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
        
        var firstLead = result.First();
        firstLead.Id.Should().Be(1);
        firstLead.ContactFirstName.Should().Be("John");
        firstLead.ContactLastName.Should().Be("Doe");
        firstLead.ContactEmail.Should().Be("john.doe@example.com");
        firstLead.ContactPhone.Should().Be("+1234567890");
        firstLead.ContactFullName.Should().Be("John Doe");
        firstLead.Status.Should().Be("Accepted");
        firstLead.Price.Should().Be(450);
        firstLead.Category.Should().Be("Plumbing");
        firstLead.Suburb.Should().Be("Downtown");
        firstLead.Description.Should().Be("Fix kitchen sink");

        var secondLead = result.Skip(1).First();
        secondLead.Id.Should().Be(2);
        secondLead.ContactFirstName.Should().Be("Jane");
        secondLead.ContactLastName.Should().Be("Smith");
        secondLead.ContactEmail.Should().Be("jane.smith@example.com");
        secondLead.ContactPhone.Should().Be("+0987654321");
        secondLead.ContactFullName.Should().Be("Jane Smith");
        secondLead.Status.Should().Be("Accepted");
        secondLead.Price.Should().Be(675);
        secondLead.Category.Should().Be("Electrical");
        secondLead.Suburb.Should().Be("Uptown");
        secondLead.Description.Should().Be("Install new outlets");
    }

    [Fact]
    public async Task Handle_WhenNoAcceptedLeads_ShouldReturnEmptyList()
    {
        // Arrange
        _mockLeadRepository.Setup(x => x.GetByStatusAsync(LeadStatus.Accepted))
            .ReturnsAsync(new List<Lead>());

        var query = new GetAcceptedLeadsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
}

