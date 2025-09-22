using System;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LeadManager.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Lead> Leads { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da entidade Lead
        modelBuilder.Entity<Lead>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.ContactFirstName)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(e => e.ContactLastName)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(e => e.ContactEmail)
                .IsRequired()
                .HasMaxLength(200);
                
            entity.Property(e => e.ContactPhone)
                .IsRequired()
                .HasMaxLength(20);
                
            entity.Property(e => e.Suburb)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000);
                
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18,2)");
                
            entity.Property(e => e.Status)
                .HasConversion<int>();
                
            entity.Property(e => e.DateCreated)
                .IsRequired();
        });

        // Dados de exemplo (seed data)
        var seedReferenceDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Lead>().HasData(
            new Lead
            {
                Id = 1,
                ContactFirstName = "John",
                ContactLastName = "Smith",
                ContactEmail = "john.smith@email.com",
                ContactPhone = "+1-555-0123",
                DateCreated = seedReferenceDate.AddDays(-5),
                Suburb = "Sydney CBD",
                Category = "Plumbing",
                Description = "Kitchen sink repair needed urgently",
                Price = 250.00m,
                Status = LeadStatus.Invited
            },
            new Lead
            {
                Id = 2,
                ContactFirstName = "Sarah",
                ContactLastName = "Johnson",
                ContactEmail = "sarah.johnson@email.com",
                ContactPhone = "+1-555-0456",
                DateCreated = seedReferenceDate.AddDays(-3),
                Suburb = "Melbourne",
                Category = "Electrical",
                Description = "Install new light fixtures in living room",
                Price = 750.00m,
                Status = LeadStatus.Invited
            },
            new Lead
            {
                Id = 3,
                ContactFirstName = "Mike",
                ContactLastName = "Brown",
                ContactEmail = "mike.brown@email.com",
                ContactPhone = "+1-555-0789",
                DateCreated = seedReferenceDate.AddDays(-7),
                Suburb = "Brisbane",
                Category = "Carpentry",
                Description = "Build custom bookshelf for home office",
                Price = 1200.00m,
                Status = LeadStatus.Accepted
            },
            new Lead
            {
                Id = 4,
                ContactFirstName = "Emma",
                ContactLastName = "Wilson",
                ContactEmail = "emma.wilson@email.com",
                ContactPhone = "+1-555-0321",
                DateCreated = seedReferenceDate.AddDays(-2),
                Suburb = "Perth",
                Category = "Painting",
                Description = "Paint exterior walls of house",
                Price = 450.00m,
                Status = LeadStatus.Invited
            }
        );
    }
}

