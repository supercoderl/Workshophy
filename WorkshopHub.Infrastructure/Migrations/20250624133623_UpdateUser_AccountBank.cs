using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser_AccountBank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountBank",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7e3892c0-9374-49fa-a3fd-53db637a40ae"),
                column: "AccountBank",
                value: "1111111111111");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountBank",
                table: "Users");
        }
    }
}
