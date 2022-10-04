using Microsoft.EntityFrameworkCore.Migrations;

namespace AthletesRestAPI.Migrations
{
    public partial class ImagesDiscipline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Disciplines",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Disciplines");
        }
    }
}
