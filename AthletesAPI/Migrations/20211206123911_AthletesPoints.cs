using Microsoft.EntityFrameworkCore.Migrations;

namespace AthletesRestAPI.Migrations
{
    public partial class AthletesPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Athletes",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "Athletes");
        }
    }
}
