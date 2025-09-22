using LeadManager.Application.Common.Interfaces;
using LeadManager.Application.Leads.Interfaces;
using MediatR;
using LeadManager.Infrastructure.Data;
using LeadManager.Infrastructure.Repositories;
using LeadManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<ILeadRepository, LeadRepository>();
builder.Services.AddScoped<INotificationService, FileNotificationService>();

// Configure MediatR
builder.Services.AddMediatR(typeof(LeadManager.Application.AssemblyReference).Assembly);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    if (app.Environment.IsDevelopment())
    {
        await AppDbContextSeed.SeedAsync(context);
    }
}

app.Run();
