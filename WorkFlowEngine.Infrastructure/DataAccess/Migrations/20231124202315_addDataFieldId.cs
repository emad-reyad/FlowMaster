using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addDataFieldId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_transitionConditions_Name_Value_Operator",
                table: "transitionConditions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "transitionConditions");

            migrationBuilder.AddColumn<int>(
                name: "DataFieldId",
                table: "transitionConditions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_transitionConditions_DataFieldId_Value_Operator",
                table: "transitionConditions",
                columns: new[] { "DataFieldId", "Value", "Operator" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_transitionConditions_DataFields_DataFieldId",
                table: "transitionConditions",
                column: "DataFieldId",
                principalTable: "DataFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transitionConditions_DataFields_DataFieldId",
                table: "transitionConditions");

            migrationBuilder.DropIndex(
                name: "IX_transitionConditions_DataFieldId_Value_Operator",
                table: "transitionConditions");

            migrationBuilder.DropColumn(
                name: "DataFieldId",
                table: "transitionConditions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "transitionConditions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_transitionConditions_Name_Value_Operator",
                table: "transitionConditions",
                columns: new[] { "Name", "Value", "Operator" },
                unique: true);
        }
    }
}
