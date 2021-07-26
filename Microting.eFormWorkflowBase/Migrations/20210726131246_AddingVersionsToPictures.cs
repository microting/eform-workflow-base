using Microsoft.EntityFrameworkCore.Migrations;

namespace Microting.eFormWorkflowBase.Migrations
{
    public partial class AddingVersionsToPictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "PicturesOfTasks",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "PicturesOfTasks",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "PicturesOfTaskDone",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "PicturesOfTaskDone",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "PicturesOfTasks");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "PicturesOfTasks");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "PicturesOfTaskDone");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "PicturesOfTaskDone");
        }
    }
}
