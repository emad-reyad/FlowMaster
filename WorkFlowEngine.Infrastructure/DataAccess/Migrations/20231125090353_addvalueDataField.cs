using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addvalueDataField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ValueDataFieldId",
                table: "ActivityDestinationTypes",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDestinationTypes_ValueDataFieldId",
                table: "ActivityDestinationTypes",
                column: "ValueDataFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityDestinationTypes_DataFields_ValueDataFieldId",
                table: "ActivityDestinationTypes",
                column: "ValueDataFieldId",
                principalTable: "DataFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityDestinationTypes_DataFields_ValueDataFieldId",
                table: "ActivityDestinationTypes");

            migrationBuilder.DropIndex(
                name: "IX_ActivityDestinationTypes_ValueDataFieldId",
                table: "ActivityDestinationTypes");

            migrationBuilder.DropColumn(
                name: "ValueDataFieldId",
                table: "ActivityDestinationTypes");
        }
    }
}
