using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class JoinProcessIsnatanceAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ProcessInstanceDataField",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstanceUsers_ProcessInstanceID",
                table: "ProcessInstanceUsers",
                column: "ProcessInstanceID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessInstanceUsers_ProcessInstances_ProcessInstanceID",
                table: "ProcessInstanceUsers",
                column: "ProcessInstanceID",
                principalTable: "ProcessInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessInstanceUsers_ProcessInstances_ProcessInstanceID",
                table: "ProcessInstanceUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProcessInstanceUsers_ProcessInstanceID",
                table: "ProcessInstanceUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ProcessInstanceDataField",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true);
        }
    }
}
