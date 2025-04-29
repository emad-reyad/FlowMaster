using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addUniqueKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ActivitiesActions_ActionId",
                table: "ActivitiesActions");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesActions_ActionId_ActivityId",
                table: "ActivitiesActions",
                columns: new[] { "ActionId", "ActivityId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ActivitiesActions_ActionId_ActivityId",
                table: "ActivitiesActions");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesActions_ActionId",
                table: "ActivitiesActions",
                column: "ActionId");
        }
    }
}
