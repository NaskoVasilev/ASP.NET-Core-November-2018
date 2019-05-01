using Microsoft.EntityFrameworkCore.Migrations;

namespace FunApp.Data.Migrations
{
    public partial class AddJokeRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatingVotes",
                table: "Jokes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalRating",
                table: "Jokes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingVotes",
                table: "Jokes");

            migrationBuilder.DropColumn(
                name: "TotalRating",
                table: "Jokes");
        }
    }
}
