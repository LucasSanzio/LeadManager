using FluentAssertions;
using LeadManager.Infrastructure.Services;

namespace LeadManager.Tests.Infrastructure;

public class FileNotificationServiceTests : IDisposable
{
    private readonly string _testFilePath;
    private readonly FileNotificationService _service;

    public FileNotificationServiceTests()
    {
        _testFilePath = Path.Combine(Path.GetTempPath(), $"test_notifications_{Guid.NewGuid()}.log");
        _service = new FileNotificationService(_testFilePath);
    }

    [Fact]
    public async Task SendNotificationAsync_ShouldWriteNotificationToFile()
    {
        // Arrange
        var to = "john.doe@example.com";
        var subject = "Lead Accepted!";
        var body = "Your lead for Plumbing in Downtown has been accepted. Final price: $450.00";

        // Act
        await _service.SendNotificationAsync(to, subject, body);

        // Assert
        File.Exists(_testFilePath).Should().BeTrue();
        
        var fileContent = await File.ReadAllTextAsync(_testFilePath);
        fileContent.Should().Contain("To: vendas@test.com");
        fileContent.Should().Contain($"Subject: {subject}");
        fileContent.Should().Contain($"Body: {body}");
        fileContent.Should().Contain("Timestamp:");
    }

    [Fact]
public async Task SendNotificationAsync_WhenCalledMultipleTimes_ShouldAppendToFile()
{
    // Arrange
    var notification1 = ("user1@example.com", "Subject 1", "Body 1");
    var notification2 = ("user2@example.com", "Subject 2", "Body 2");

    // Act
    await _service.SendNotificationAsync(notification1.Item1, notification1.Item2, notification1.Item3);
    await _service.SendNotificationAsync(notification2.Item1, notification2.Item2, notification2.Item3);

    // Assert
    var fileContent = await File.ReadAllTextAsync(_testFilePath);
        
    // Agora sempre deve conter o email fixo de vendas
    fileContent.Should().Contain("To: vendas@test.com");

    // Mas ainda deve conter os subjects e bodies corretos
    fileContent.Should().Contain("Subject 1");
    fileContent.Should().Contain("Body 1");
    fileContent.Should().Contain("Subject 2");
    fileContent.Should().Contain("Body 2");

    // Deve ter pelo menos duas seções
    fileContent.Split("---").Should().HaveCountGreaterThan(2);
}


    [Fact]
    public async Task SendNotificationAsync_ShouldCreateDirectoryIfNotExists()
    {
        // Arrange
        var nonExistentDir = Path.Combine(Path.GetTempPath(), $"test_dir_{Guid.NewGuid()}");
        var filePath = Path.Combine(nonExistentDir, "notifications.log");
        var service = new FileNotificationService(filePath);

        // Act
        await service.SendNotificationAsync("test@example.com", "Test", "Test body");

        // Assert
        Directory.Exists(nonExistentDir).Should().BeTrue();
        File.Exists(filePath).Should().BeTrue();

        // Cleanup
        Directory.Delete(nonExistentDir, true);
    }

    public void Dispose()
    {
        if (File.Exists(_testFilePath))
        {
            File.Delete(_testFilePath);
        }
    }
}

