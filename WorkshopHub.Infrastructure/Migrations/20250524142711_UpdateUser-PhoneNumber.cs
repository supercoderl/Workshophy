using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7e3892c0-9374-49fa-a3fd-53db637a40ae"),
                column: "PhoneNumber",
                value: "+1 204 287 291");

            migrationBuilder.CreateIndex(
                name: "IX_Workshops_OrganizerId",
                table: "Workshops",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workshop_User_OrganizerId",
                table: "Workshops",
                column: "OrganizerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workshop_User_OrganizerId",
                table: "Workshops");

            migrationBuilder.DropIndex(
                name: "IX_Workshops_OrganizerId",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");
        }
    }
}
