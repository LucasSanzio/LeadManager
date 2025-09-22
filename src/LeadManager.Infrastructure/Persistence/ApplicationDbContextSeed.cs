using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using LeadManager.Infrastructure.Data;

namespace LeadManager.Infrastructure.Data
{
    public static class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Leads.Any())
            {
                var leads = new List<Lead>
                {
                    new Lead { ContactFirstName = "Lucas", ContactLastName = "Ribeiro", ContactEmail="lucas@example.com", ContactPhone="111-1111", Suburb="Centro", Category="Plumbing", Description="Pipe issue", Price=600m, Status=LeadStatus.Invited },
                    new Lead { ContactFirstName = "Maria", ContactLastName = "Silva", ContactEmail="maria@example.com", ContactPhone="222-2222", Suburb="Zona Sul", Category="Cleaning", Description="Apartment cleaning", Price=300m, Status=LeadStatus.Invited },
                    new Lead { ContactFirstName = "João", ContactLastName = "Santos", ContactEmail="joao@example.com", ContactPhone="333-3333", Suburb="Zona Norte", Category="Electrical", Description="Fix wiring", Price=800m, Status=LeadStatus.Invited }
                };

                context.Leads.AddRange(leads);
                await context.SaveChangesAsync();
            }
        }
    }
}
