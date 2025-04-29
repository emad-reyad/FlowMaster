using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessDataFields_Processes_ProcessID",
                table: "ProcessDataFields");

            migrationBuilder.RenameColumn(
                name: "ProcessID",
                table: "ProcessDataFields",
                newName: "ProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessDataFields_ProcessID",
                table: "ProcessDataFields",
                newName: "IX_ProcessDataFields_ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessDataFields_Processes_ProcessId",
                table: "ProcessDataFields",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessDataFields_Processes_ProcessId",
                table: "ProcessDataFields");

            migrationBuilder.RenameColumn(
                name: "ProcessId",
                table: "ProcessDataFields",
                newName: "ProcessID");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessDataFields_ProcessId",
                table: "ProcessDataFields",
                newName: "IX_ProcessDataFields_ProcessID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessDataFields_Processes_ProcessID",
                table: "ProcessDataFields",
                column: "ProcessID",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
