using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_processInstanceHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "processInstanceHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessInstanceID = table.Column<int>(type: "int", nullable: false),
                    CurrentActivityId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    NextActivityId = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_processInstanceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_processInstanceHistories_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_processInstanceHistories_Activities_CurrentActivityId",
                        column: x => x.CurrentActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_processInstanceHistories_Activities_NextActivityId",
                        column: x => x.NextActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_processInstanceHistories_ActionId",
                table: "processInstanceHistories",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_processInstanceHistories_CurrentActivityId",
                table: "processInstanceHistories",
                column: "CurrentActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_processInstanceHistories_NextActivityId",
                table: "processInstanceHistories",
                column: "NextActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "processInstanceHistories");
        }
    }
}
