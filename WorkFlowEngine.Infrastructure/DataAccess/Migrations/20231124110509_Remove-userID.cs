using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveuserID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityDestinationTypes_DestinationTypes_DestinationTypeId",
                table: "ActivityDestinationTypes");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "ActivityDestinationTypes");

            migrationBuilder.RenameColumn(
                name: "DestinationTypeId",
                table: "ActivityDestinationTypes",
                newName: "DestinationTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityDestinationTypes_DestinationTypeId",
                table: "ActivityDestinationTypes",
                newName: "IX_ActivityDestinationTypes_DestinationTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityDestinationTypes_DestinationTypes_DestinationTypeID",
                table: "ActivityDestinationTypes",
                column: "DestinationTypeID",
                principalTable: "DestinationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityDestinationTypes_DestinationTypes_DestinationTypeID",
                table: "ActivityDestinationTypes");

            migrationBuilder.RenameColumn(
                name: "DestinationTypeID",
                table: "ActivityDestinationTypes",
                newName: "DestinationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityDestinationTypes_DestinationTypeID",
                table: "ActivityDestinationTypes",
                newName: "IX_ActivityDestinationTypes_DestinationTypeId");

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "ActivityDestinationTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityDestinationTypes_DestinationTypes_DestinationTypeId",
                table: "ActivityDestinationTypes",
                column: "DestinationTypeId",
                principalTable: "DestinationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
