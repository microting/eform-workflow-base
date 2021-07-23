using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microting.eFormWorkflowBase.Migrations
{
    public partial class FixingRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "WorkflowCaseVersions",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<int>(
                name: "IncidentPlaceId",
                table: "WorkflowCaseVersions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IncidentTypeId",
                table: "WorkflowCaseVersions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "WorkflowCases",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<int>(
                name: "IncidentPlaceId",
                table: "WorkflowCases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IncidentTypeId",
                table: "WorkflowCases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncidentPlaceId",
                table: "WorkflowCaseVersions");

            migrationBuilder.DropColumn(
                name: "IncidentTypeId",
                table: "WorkflowCaseVersions");

            migrationBuilder.DropColumn(
                name: "IncidentPlaceId",
                table: "WorkflowCases");

            migrationBuilder.DropColumn(
                name: "IncidentTypeId",
                table: "WorkflowCases");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "WorkflowCaseVersions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "WorkflowCases",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);
        }
    }
}
