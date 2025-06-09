using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ActivityPointRules",
                columns: new[] { "Id", "ActivityPoint", "ActivityType", "DeletedAt" },
                values: new object[] { new Guid("9c052706-7218-4d52-a5ad-4e7071eba219"), 10, "AdtendWorkshop", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivityPointRules",
                keyColumn: "Id",
                keyValue: new Guid("9c052706-7218-4d52-a5ad-4e7071eba219"));
        }
    }
}
