using Microsoft.EntityFrameworkCore.Migrations;

namespace Microting.eFormWorkflowBase.Migrations
{
    public partial class AddingExtraAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBySiteId",
                table: "WorkflowCaseVersions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBySiteName",
                table: "WorkflowCaseVersions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPhotos",
                table: "WorkflowCaseVersions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBySiteId",
                table: "WorkflowCases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBySiteName",
                table: "WorkflowCases",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPhotos",
                table: "WorkflowCases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBySiteId",
                table: "WorkflowCaseVersions");

            migrationBuilder.DropColumn(
                name: "CreatedBySiteName",
                table: "WorkflowCaseVersions");

            migrationBuilder.DropColumn(
                name: "NumberOfPhotos",
                table: "WorkflowCaseVersions");

            migrationBuilder.DropColumn(
                name: "CreatedBySiteId",
                table: "WorkflowCases");

            migrationBuilder.DropColumn(
                name: "CreatedBySiteName",
                table: "WorkflowCases");

            migrationBuilder.DropColumn(
                name: "NumberOfPhotos",
                table: "WorkflowCases");
        }
    }
}
