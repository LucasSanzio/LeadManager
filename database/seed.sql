USE [LeadManagerDb];
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Leads')
BEGIN
    PRINT 'The Leads table is not present. Run the EF Core migrations before executing this seed script.';
    RETURN;
END

IF NOT EXISTS (SELECT 1 FROM Leads)
BEGIN
    SET IDENTITY_INSERT Leads ON;

    INSERT INTO Leads (Id, ContactFirstName, ContactLastName, ContactEmail, ContactPhone, DateCreated, Suburb, Category, Description, Price, Status)
    VALUES
        (1, 'John', 'Smith', 'john.smith@email.com', '+1-555-0123', DATEADD(DAY, -5, SYSUTCDATETIME()), 'Sydney CBD', 'Plumbing', 'Kitchen sink repair needed urgently', 250.00, 1),
        (2, 'Sarah', 'Johnson', 'sarah.johnson@email.com', '+1-555-0456', DATEADD(DAY, -3, SYSUTCDATETIME()), 'Melbourne', 'Electrical', 'Install new light fixtures in living room', 750.00, 1),
        (3, 'Mike', 'Brown', 'mike.brown@email.com', '+1-555-0789', DATEADD(DAY, -7, SYSUTCDATETIME()), 'Brisbane', 'Carpentry', 'Build custom bookshelf for home office', 1200.00, 2),
        (4, 'Emma', 'Wilson', 'emma.wilson@email.com', '+1-555-0321', DATEADD(DAY, -2, SYSUTCDATETIME()), 'Perth', 'Painting', 'Paint exterior walls of house', 450.00, 1);

    SET IDENTITY_INSERT Leads OFF;

    INSERT INTO Leads (ContactFirstName, ContactLastName, ContactEmail, ContactPhone, DateCreated, Suburb, Category, Description, Price, Status)
    VALUES
        ('Lucas', 'Ribeiro', 'lucas@example.com', '111-1111', SYSUTCDATETIME(), 'Centro', 'Plumbing', 'Pipe issue', 600.00, 1),
        ('Maria', 'Silva', 'maria@example.com', '222-2222', SYSUTCDATETIME(), 'Zona Sul', 'Cleaning', 'Apartment cleaning', 300.00, 1),
        ('João', 'Santos', 'joao@example.com', '333-3333', SYSUTCDATETIME(), 'Zona Norte', 'Electrical', 'Fix wiring', 800.00, 1);
END
ELSE
BEGIN
    PRINT 'Seed data skipped because the Leads table already contains records.';
END
GO
