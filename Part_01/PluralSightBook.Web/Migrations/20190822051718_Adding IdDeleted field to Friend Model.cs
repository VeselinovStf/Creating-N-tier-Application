using Microsoft.EntityFrameworkCore.Migrations;

namespace PluralSightBook.Web.Migrations
{
    public partial class AddingIdDeletedfieldtoFriendModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Friends",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Friends");
        }
    }
}
