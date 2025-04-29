using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class modifyUniqIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_transitionConditions_DataFieldId_Value_Operator",
                table: "transitionConditions");

            migrationBuilder.DropIndex(
                name: "IX_transitionConditions_TransitionId",
                table: "transitionConditions");

            migrationBuilder.CreateIndex(
                name: "IX_transitionConditions_DataFieldId",
                table: "transitionConditions",
                column: "DataFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_transitionConditions_TransitionId_DataFieldId_Value_Operator",
                table: "transitionConditions",
                columns: new[] { "TransitionId", "DataFieldId", "Value", "Operator" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_transitionConditions_DataFieldId",
                table: "transitionConditions");

            migrationBuilder.DropIndex(
                name: "IX_transitionConditions_TransitionId_DataFieldId_Value_Operator",
                table: "transitionConditions");

            migrationBuilder.CreateIndex(
                name: "IX_transitionConditions_DataFieldId_Value_Operator",
                table: "transitionConditions",
                columns: new[] { "DataFieldId", "Value", "Operator" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transitionConditions_TransitionId",
                table: "transitionConditions",
                column: "TransitionId");
        }
    }
}
