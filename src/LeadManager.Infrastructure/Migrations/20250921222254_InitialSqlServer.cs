using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadManager.Infrastructure.Migrations
{
    public partial class InitialSqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactFirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactLastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Leads",
                columns: new[] { "Id", "Category", "ContactEmail", "ContactFirstName", "ContactLastName", "ContactPhone", "DateCreated", "Description", "Price", "Status", "Suburb" },
                values: new object[,]
                {
                    { 1, "Plumbing", "john.smith@email.com", "John", "Smith", "+1-555-0123", new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Kitchen sink repair needed urgently", 250.00m, 1, "Sydney CBD" },
                    { 2, "Electrical", "sarah.johnson@email.com", "Sarah", "Johnson", "+1-555-0456", new DateTime(2023, 12, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Install new light fixtures in living room", 750.00m, 1, "Melbourne" },
                    { 3, "Carpentry", "mike.brown@email.com", "Mike", "Brown", "+1-555-0789", new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Build custom bookshelf for home office", 1200.00m, 2, "Brisbane" },
                    { 4, "Painting", "emma.wilson@email.com", "Emma", "Wilson", "+1-555-0321", new DateTime(2023, 12, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Paint exterior walls of house", 450.00m, 1, "Perth" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leads");
        }
    }
}
