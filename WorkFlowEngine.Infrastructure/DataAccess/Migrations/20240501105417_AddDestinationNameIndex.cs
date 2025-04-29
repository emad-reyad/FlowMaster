using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDestinationNameIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DestinationName",
                table: "ProcessInstanceUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstanceUsers_DestinationName",
                table: "ProcessInstanceUsers",
                column: "DestinationName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProcessInstanceUsers_DestinationName",
                table: "ProcessInstanceUsers");

            migrationBuilder.AlterColumn<string>(
                name: "DestinationName",
                table: "ProcessInstanceUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
