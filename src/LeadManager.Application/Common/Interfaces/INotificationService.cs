using System.Threading.Tasks;

namespace LeadManager.Application.Common.Interfaces;

public interface INotificationService
{
    Task SendNotificationAsync(string to, string subject, string body);
}

