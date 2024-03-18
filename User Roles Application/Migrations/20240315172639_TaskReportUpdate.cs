using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Roles_Application.Migrations
{
    /// <inheritdoc />
    public partial class TaskReportUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "TaskReport");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "TaskReport",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
