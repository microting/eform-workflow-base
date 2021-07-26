using Microsoft.EntityFrameworkCore.Migrations;

namespace Microting.eFormWorkflowBase.Migrations
{
    public partial class AddingUploadedDataId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UploadedDataId",
                table: "PicturesOfTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UploadedDataId",
                table: "PicturesOfTaskDone",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedDataId",
                table: "PicturesOfTasks");

            migrationBuilder.DropColumn(
                name: "UploadedDataId",
                table: "PicturesOfTaskDone");
        }
    }
}
