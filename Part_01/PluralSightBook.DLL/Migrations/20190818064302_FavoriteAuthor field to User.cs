using Microsoft.EntityFrameworkCore.Migrations;

namespace PluralSightBook.DLL.Migrations
{
    public partial class FavoriteAuthorfieldtoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavoriteAuthor",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoriteAuthor",
                table: "AspNetUsers");
        }
    }
}