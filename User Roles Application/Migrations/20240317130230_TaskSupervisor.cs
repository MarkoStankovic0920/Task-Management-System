using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Roles_Application.Migrations
{
    /// <inheritdoc />
    public partial class TaskSupervisor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Supervisor",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Supervisor",
                table: "Tasks");
        }
    }
}
