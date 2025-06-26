using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkshopScheduleRemoveDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationMinutes",
                table: "Workshops");

            migrationBuilder.AddColumn<string>(
                name: "ScheduleStatus",
                table: "WorkshopSchedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddCheckConstraint(
                name: "CK_WorkshopSchedule_ScheduleStatus",
                table: "WorkshopSchedules",
                sql: "[ScheduleStatus] IN ('Pending', 'Starting', 'Ended')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_WorkshopSchedule_ScheduleStatus",
                table: "WorkshopSchedules");

            migrationBuilder.DropColumn(
                name: "ScheduleStatus",
                table: "WorkshopSchedules");

            migrationBuilder.AddColumn<int>(
                name: "DurationMinutes",
                table: "Workshops",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
