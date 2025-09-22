using FluentAssertions;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;

namespace LeadManager.Tests.Domain;

public class LeadTests
{
    [Fact]
    public void Accept_ShouldApplyDiscount_WhenPriceGreaterThan500()
    {
        // Arrange
        var lead = new Lead
        {
            ContactFirstName = "John",
            ContactLastName = "Doe",
            ContactEmail = "john@test.com",
            ContactPhone = "123456",
            Category = "Category",
            Description = "Desc",
            Price = 600m,
            Suburb = "Suburb"
        };

        // Act
        lead.Accept();

        // Assert
        lead.Price.Should().Be(540m); // 10% discount
        lead.Status.Should().Be(LeadStatus.Accepted);
    }

    [Fact]
    public void Accept_ShouldNotApplyDiscount_WhenPriceLessOrEqual500()
    {
        var lead = new Lead
        {
            ContactFirstName = "Jane",
            ContactLastName = "Smith",
            ContactEmail = "jane@test.com",
            ContactPhone = "123456",
            Category = "Category",
            Description = "Desc",
            Price = 500m,
            Suburb = "Suburb"
        };

        lead.Accept();

        lead.Price.Should().Be(500m);
        lead.Status.Should().Be(LeadStatus.Accepted);
    }

    [Fact]
    public void Decline_ShouldUpdateStatusToDeclined()
    {
        var lead = new Lead
        {
            ContactFirstName = "Joe",
            ContactLastName = "Bloggs",
            ContactEmail = "joe@test.com",
            ContactPhone = "123456",
            Category = "Category",
            Description = "Desc",
            Price = 300m,
            Suburb = "Suburb"
        };

        lead.Decline();

        lead.Status.Should().Be(LeadStatus.Declined);
    }

    [Fact]
    public void Accept_ShouldThrow_WhenLeadNotInvited()
    {
        var lead = new Lead { Price = 400m, Status = LeadStatus.Accepted };

        Action act = () => lead.Accept();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Only invited leads can be accepted.");
    }

    [Fact]
    public void Decline_ShouldThrow_WhenLeadNotInvited()
    {
        var lead = new Lead { Price = 400m, Status = LeadStatus.Declined };

        Action act = () => lead.Decline();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Only invited leads can be declined.");
    }
}
