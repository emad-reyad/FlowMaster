using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlowEngine.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class revert_back : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityDestinationTypes_DestinationTypes_DestinationTypeId",
                table: "ActivityDestinationTypes");

            migrationBuilder.RenameColumn(
                name: "DestinationTypeId",
                table: "ActivityDestinationTypes",
                newName: "UserTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityDestinationTypes_DestinationTypeId",
                table: "ActivityDestinationTypes",
                newName: "IX_ActivityDestinationTypes_UserTypeId");

            migrationBuilder.AddColumn<string>(
                name: "ActivityType",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProcessId",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataFields_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcessInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    CurrentActivityId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOpened = table.Column<bool>(type: "bit", nullable: false),
                    OpnenedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessInstances_Activities_CurrentActivityId",
                        column: x => x.CurrentActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessInstances_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessInstanceUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessInstanceID = table.Column<int>(type: "int", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessInstanceUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessInstanceUsers_DestinationTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "DestinationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivitiesActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitiesActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivitiesActions_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivitiesActions_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentActivityId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    NextActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transitions_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transitions_Activities_CurrentActivityId",
                        column: x => x.CurrentActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_transitions_Activities_NextActivityId",
                        column: x => x.NextActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "transitionConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransitionId = table.Column<int>(type: "int", nullable: false),
                    BinaryOperator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Operator = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transitionConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transitionConditions_transitions_TransitionId",
                        column: x => x.TransitionId,
                        principalTable: "transitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ProcessId",
                table: "Activities",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesActions_ActionId",
                table: "ActivitiesActions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesActions_ActivityId",
                table: "ActivitiesActions",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_DataFields_ProcessId",
                table: "DataFields",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstances_CurrentActivityId",
                table: "ProcessInstances",
                column: "CurrentActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstances_ProcessId",
                table: "ProcessInstances",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstanceUsers_UserTypeId",
                table: "ProcessInstanceUsers",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_transitionConditions_Name_Value_Operator",
                table: "transitionConditions",
                columns: new[] { "Name", "Value", "Operator" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transitionConditions_TransitionId",
                table: "transitionConditions",
                column: "TransitionId");

            migrationBuilder.CreateIndex(
                name: "IX_transitions_ActionId",
                table: "transitions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_transitions_CurrentActivityId",
                table: "transitions",
                column: "CurrentActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_transitions_NextActivityId",
                table: "transitions",
                column: "NextActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Processes_ProcessId",
                table: "Activities",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityDestinationTypes_DestinationTypes_UserTypeId",
                table: "ActivityDestinationTypes",
                column: "UserTypeId",
                principalTable: "DestinationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Processes_ProcessId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityDestinationTypes_DestinationTypes_UserTypeId",
                table: "ActivityDestinationTypes");

            migrationBuilder.DropTable(
                name: "ActivitiesActions");

            migrationBuilder.DropTable(
                name: "DataFields");

            migrationBuilder.DropTable(
                name: "ProcessInstances");

            migrationBuilder.DropTable(
                name: "ProcessInstanceUsers");

            migrationBuilder.DropTable(
                name: "transitionConditions");

            migrationBuilder.DropTable(
                name: "transitions");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ProcessId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ActivityType",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "UserTypeId",
                table: "ActivityDestinationTypes",
                newName: "DestinationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityDestinationTypes_UserTypeId",
                table: "ActivityDestinationTypes",
                newName: "IX_ActivityDestinationTypes_DestinationTypeId");

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
