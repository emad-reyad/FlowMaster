using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProcessDataFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityDestinationTypes_DestinationTypes_UserTypeId",
                table: "ActivityDestinationTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_DataFields_Processes_ProcessId",
                table: "DataFields");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessInstanceUsers_DestinationTypes_UserTypeId",
                table: "ProcessInstanceUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProcessInstanceUsers_UserTypeId",
                table: "ProcessInstanceUsers");

            migrationBuilder.DropIndex(
                name: "IX_DataFields_ProcessId",
                table: "DataFields");

            migrationBuilder.DropIndex(
                name: "IX_ActivityDestinationTypes_UserTypeId",
                table: "ActivityDestinationTypes");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "DataFields");

            migrationBuilder.AddColumn<int>(
                name: "DestinationTypeId",
                table: "ProcessInstanceUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DestinationTypeId",
                table: "ActivityDestinationTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProcessDataFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessID = table.Column<int>(type: "int", nullable: false),
                    DataFieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessDataFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessDataFields_DataFields_DataFieldId",
                        column: x => x.DataFieldId,
                        principalTable: "DataFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessDataFields_Processes_ProcessID",
                        column: x => x.ProcessID,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstanceUsers_DestinationTypeId",
                table: "ProcessInstanceUsers",
                column: "DestinationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDestinationTypes_DestinationTypeId",
                table: "ActivityDestinationTypes",
                column: "DestinationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessDataFields_DataFieldId",
                table: "ProcessDataFields",
                column: "DataFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessDataFields_ProcessID",
                table: "ProcessDataFields",
                column: "ProcessID");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityDestinationTypes_DestinationTypes_DestinationTypeId",
                table: "ActivityDestinationTypes",
                column: "DestinationTypeId",
                principalTable: "DestinationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessInstanceUsers_DestinationTypes_DestinationTypeId",
                table: "ProcessInstanceUsers",
                column: "DestinationTypeId",
                principalTable: "DestinationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityDestinationTypes_DestinationTypes_DestinationTypeId",
                table: "ActivityDestinationTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessInstanceUsers_DestinationTypes_DestinationTypeId",
                table: "ProcessInstanceUsers");

            migrationBuilder.DropTable(
                name: "ProcessDataFields");

            migrationBuilder.DropIndex(
                name: "IX_ProcessInstanceUsers_DestinationTypeId",
                table: "ProcessInstanceUsers");

            migrationBuilder.DropIndex(
                name: "IX_ActivityDestinationTypes_DestinationTypeId",
                table: "ActivityDestinationTypes");

            migrationBuilder.DropColumn(
                name: "DestinationTypeId",
                table: "ProcessInstanceUsers");

            migrationBuilder.DropColumn(
                name: "DestinationTypeId",
                table: "ActivityDestinationTypes");

            migrationBuilder.AddColumn<int>(
                name: "ProcessId",
                table: "DataFields",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstanceUsers_UserTypeId",
                table: "ProcessInstanceUsers",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DataFields_ProcessId",
                table: "DataFields",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDestinationTypes_UserTypeId",
                table: "ActivityDestinationTypes",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityDestinationTypes_DestinationTypes_UserTypeId",
                table: "ActivityDestinationTypes",
                column: "UserTypeId",
                principalTable: "DestinationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DataFields_Processes_ProcessId",
                table: "DataFields",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessInstanceUsers_DestinationTypes_UserTypeId",
                table: "ProcessInstanceUsers",
                column: "UserTypeId",
                principalTable: "DestinationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
