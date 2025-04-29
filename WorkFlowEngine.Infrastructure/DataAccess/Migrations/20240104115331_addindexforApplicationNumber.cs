using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addindexforApplicationNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_processInstanceHistories_ProcessInstances_ProcessInstanceID",
                table: "processInstanceHistories");

            migrationBuilder.RenameColumn(
                name: "ProcessInstanceID",
                table: "processInstanceHistories",
                newName: "ProcessInstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_processInstanceHistories_ProcessInstanceID",
                table: "processInstanceHistories",
                newName: "IX_processInstanceHistories_ProcessInstanceId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationNumber",
                table: "ProcessInstances",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstances_ApplicationNumber",
                table: "ProcessInstances",
                column: "ApplicationNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_processInstanceHistories_ProcessInstances_ProcessInstanceId",
                table: "processInstanceHistories",
                column: "ProcessInstanceId",
                principalTable: "ProcessInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_processInstanceHistories_ProcessInstances_ProcessInstanceId",
                table: "processInstanceHistories");

            migrationBuilder.DropIndex(
                name: "IX_ProcessInstances_ApplicationNumber",
                table: "ProcessInstances");

            migrationBuilder.RenameColumn(
                name: "ProcessInstanceId",
                table: "processInstanceHistories",
                newName: "ProcessInstanceID");

            migrationBuilder.RenameIndex(
                name: "IX_processInstanceHistories_ProcessInstanceId",
                table: "processInstanceHistories",
                newName: "IX_processInstanceHistories_ProcessInstanceID");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationNumber",
                table: "ProcessInstances",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_processInstanceHistories_ProcessInstances_ProcessInstanceID",
                table: "processInstanceHistories",
                column: "ProcessInstanceID",
                principalTable: "ProcessInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
