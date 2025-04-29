using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addrequiredOrnotToProcessDataField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultValue",
                table: "DataFields");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "DataFields");

            migrationBuilder.RenameColumn(
                name: "IsOpened",
                table: "ProcessInstances",
                newName: "Opnened");

            migrationBuilder.AlterColumn<string>(
                name: "OpnenedBy",
                table: "ProcessInstances",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DefaultValue",
                table: "ProcessDataFields",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "ProcessDataFields",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ProcessInstanceDataField",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessInstanceId = table.Column<int>(type: "int", nullable: false),
                    DataFieldId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessInstanceDataField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessInstanceDataField_DataFields_DataFieldId",
                        column: x => x.DataFieldId,
                        principalTable: "DataFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessInstanceDataField_ProcessInstances_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalTable: "ProcessInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstanceDataField_DataFieldId",
                table: "ProcessInstanceDataField",
                column: "DataFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstanceDataField_ProcessInstanceId",
                table: "ProcessInstanceDataField",
                column: "ProcessInstanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessInstanceDataField");

            migrationBuilder.DropColumn(
                name: "DefaultValue",
                table: "ProcessDataFields");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "ProcessDataFields");

            migrationBuilder.RenameColumn(
                name: "Opnened",
                table: "ProcessInstances",
                newName: "IsOpened");

            migrationBuilder.AlterColumn<string>(
                name: "OpnenedBy",
                table: "ProcessInstances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultValue",
                table: "DataFields",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "DataFields",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
