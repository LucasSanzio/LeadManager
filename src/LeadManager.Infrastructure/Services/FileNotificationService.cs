using LeadManager.Application.Common.Interfaces;

namespace LeadManager.Infrastructure.Services;

public class FileNotificationService : INotificationService
{
    private readonly string _logFilePath;

    public FileNotificationService()
    {
        _logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "notifications.log");
    }

    public FileNotificationService(string filePath)
    {
        _logFilePath = filePath;
    }

    public async Task SendNotificationAsync(string to, string subject, string body)
{
    // Enforce single sales email as recipient per requirement
    var salesEmail = "vendas@test.com";

    var directory = Path.GetDirectoryName(_logFilePath);
    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
    {
        Directory.CreateDirectory(directory);
    }

    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    var notificationContent =
        $"Timestamp: {timestamp}\nTo: {salesEmail}\nSubject: {subject}\nBody: {body}\n---\n\n";

    await File.AppendAllTextAsync(_logFilePath, notificationContent);
}
}
