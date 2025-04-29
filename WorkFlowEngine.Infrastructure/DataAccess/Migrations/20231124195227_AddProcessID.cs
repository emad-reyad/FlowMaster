using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProcessID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessId",
                table: "transitions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_transitions_ProcessId",
                table: "transitions",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_transitions_Processes_ProcessId",
                table: "transitions",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transitions_Processes_ProcessId",
                table: "transitions");

            migrationBuilder.DropIndex(
                name: "IX_transitions_ProcessId",
                table: "transitions");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "transitions");
        }
    }
}
