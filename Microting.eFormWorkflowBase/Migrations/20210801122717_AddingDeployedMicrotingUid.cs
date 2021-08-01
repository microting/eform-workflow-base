using Microsoft.EntityFrameworkCore.Migrations;

namespace Microting.eFormWorkflowBase.Migrations
{
    public partial class AddingDeployedMicrotingUid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeployedMicrotingUid",
                table: "WorkflowCaseVersions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeployedMicrotingUid",
                table: "WorkflowCases",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeployedMicrotingUid",
                table: "WorkflowCaseVersions");

            migrationBuilder.DropColumn(
                name: "DeployedMicrotingUid",
                table: "WorkflowCases");
        }
    }
}
