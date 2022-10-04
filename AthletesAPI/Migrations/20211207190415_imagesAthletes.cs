using Microsoft.EntityFrameworkCore.Migrations;

namespace AthletesRestAPI.Migrations
{
    public partial class imagesAthletes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Athletes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Athletes");
        }
    }
}
