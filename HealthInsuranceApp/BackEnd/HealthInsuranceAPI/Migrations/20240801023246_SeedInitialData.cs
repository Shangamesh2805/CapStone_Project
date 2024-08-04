using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthInsuranceAPI.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "AgentID", "ContactNumber", "Name", "UserID" },
                values: new object[] { new Guid("16f76b08-0f6b-4d8d-a963-d0c99769f895"), "123-456-7890", "Ram", new Guid("41a55ded-7c05-4058-9e63-1f5a8fafe4db") });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "Address", "DateOfBirth", "Name", "Phone", "UserID" },
                values: new object[,]
                {
                    { new Guid("122b2296-0995-479a-bee8-116d0ecd3c91"), "123 Main St", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arun", "555-1234", new Guid("d1c60656-dea3-4942-857f-1f4a2c715265") },
                    { new Guid("2d7be1a6-f829-43c0-86a5-98a04501b3d7"), "456 Elm St", new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shangu", "555-5678", new Guid("3fc25cd0-27c8-4a44-900f-1697b7f8171c") }
                });

            migrationBuilder.InsertData(
                table: "InsurancePolicies",
                columns: new[] { "PolicyID", "CoverageAmount", "EndDate", "PolicyName", "PolicyNumber", "PolicyType", "PremiumAmount", "RenewalAmount", "StartDate" },
                values: new object[,]
                {
                    { new Guid("7feae666-b096-4c7c-a56c-23666a2b0d57"), 500000m, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Life Insurance Plan B", "LIPB002", "Life", 2000m, 1800m, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fb90d054-1339-4d45-b968-7c46ff02f5e2"), 100000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Health Insurance Plan A", "HIPA001", "Health", 1000m, 900m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "AgentID",
                keyValue: new Guid("16f76b08-0f6b-4d8d-a963-d0c99769f895"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: new Guid("122b2296-0995-479a-bee8-116d0ecd3c91"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: new Guid("2d7be1a6-f829-43c0-86a5-98a04501b3d7"));

            migrationBuilder.DeleteData(
                table: "InsurancePolicies",
                keyColumn: "PolicyID",
                keyValue: new Guid("7feae666-b096-4c7c-a56c-23666a2b0d57"));

            migrationBuilder.DeleteData(
                table: "InsurancePolicies",
                keyColumn: "PolicyID",
                keyValue: new Guid("fb90d054-1339-4d45-b968-7c46ff02f5e2"));
        }
    }
}
