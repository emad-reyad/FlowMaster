using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyProcessActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Activities_Processes_ProcessId",
            //    table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ProcessId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "Activities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessId",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ProcessId",
                table: "Activities",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Processes_ProcessId",
                table: "Activities",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id");
        }
    }
}
