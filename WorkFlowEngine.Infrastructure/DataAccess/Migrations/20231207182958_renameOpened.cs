using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class renameOpened : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessInstanceUsers_ProcessInstances_ProcessInstanceID",
                table: "ProcessInstanceUsers");

            migrationBuilder.RenameColumn(
                name: "ProcessInstanceID",
                table: "ProcessInstanceUsers",
                newName: "ProcessInstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessInstanceUsers_ProcessInstanceID",
                table: "ProcessInstanceUsers",
                newName: "IX_ProcessInstanceUsers_ProcessInstanceId");

            migrationBuilder.RenameColumn(
                name: "OpnenedBy",
                table: "ProcessInstances",
                newName: "OpenedBy");

            migrationBuilder.RenameColumn(
                name: "Opnened",
                table: "ProcessInstances",
                newName: "Opened");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessInstanceUsers_ProcessInstances_ProcessInstanceId",
                table: "ProcessInstanceUsers",
                column: "ProcessInstanceId",
                principalTable: "ProcessInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessInstanceUsers_ProcessInstances_ProcessInstanceId",
                table: "ProcessInstanceUsers");

            migrationBuilder.RenameColumn(
                name: "ProcessInstanceId",
                table: "ProcessInstanceUsers",
                newName: "ProcessInstanceID");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessInstanceUsers_ProcessInstanceId",
                table: "ProcessInstanceUsers",
                newName: "IX_ProcessInstanceUsers_ProcessInstanceID");

            migrationBuilder.RenameColumn(
                name: "OpenedBy",
                table: "ProcessInstances",
                newName: "OpnenedBy");

            migrationBuilder.RenameColumn(
                name: "Opened",
                table: "ProcessInstances",
                newName: "Opnened");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessInstanceUsers_ProcessInstances_ProcessInstanceID",
                table: "ProcessInstanceUsers",
                column: "ProcessInstanceID",
                principalTable: "ProcessInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
