using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyRemoveSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkshopSchedules");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Workshops",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ScheduleStatus",
                table: "Workshops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Pending");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Workshops",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddCheckConstraint(
                name: "CK_Workshop_ScheduleStatus",
                table: "Workshops",
                sql: "[ScheduleStatus] IN ('Pending', 'Starting', 'Ended')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Workshop_ScheduleStatus",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "ScheduleStatus",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Workshops");

            migrationBuilder.CreateTable(
                name: "WorkshopSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkshopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduleStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopSchedules", x => x.Id);
                    table.CheckConstraint("CK_WorkshopSchedule_ScheduleStatus", "[ScheduleStatus] IN ('Pending', 'Starting', 'Ended')");
                    table.ForeignKey(
                        name: "FK_WorkshopSchedule_Workshop_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopSchedules_WorkshopId",
                table: "WorkshopSchedules",
                column: "WorkshopId");
        }
    }
}
