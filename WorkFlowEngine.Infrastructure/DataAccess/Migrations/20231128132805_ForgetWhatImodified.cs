using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ForgetWhatImodified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "ProcessInstanceUsers");

            migrationBuilder.RenameColumn(
                name: "Destination",
                table: "ProcessInstanceUsers",
                newName: "DestinationName");

            migrationBuilder.CreateIndex(
                name: "IX_processInstanceHistories_ProcessInstanceID",
                table: "processInstanceHistories",
                column: "ProcessInstanceID");

            migrationBuilder.AddForeignKey(
                name: "FK_processInstanceHistories_ProcessInstances_ProcessInstanceID",
                table: "processInstanceHistories",
                column: "ProcessInstanceID",
                principalTable: "ProcessInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_processInstanceHistories_ProcessInstances_ProcessInstanceID",
                table: "processInstanceHistories");

            migrationBuilder.DropIndex(
                name: "IX_processInstanceHistories_ProcessInstanceID",
                table: "processInstanceHistories");

            migrationBuilder.RenameColumn(
                name: "DestinationName",
                table: "ProcessInstanceUsers",
                newName: "Destination");

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "ProcessInstanceUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
