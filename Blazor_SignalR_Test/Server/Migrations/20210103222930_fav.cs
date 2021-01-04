using Microsoft.EntityFrameworkCore.Migrations;

namespace Blazor_SignalR_Test.Server.Migrations
{
    public partial class fav : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "favorite",
                table: "Coins",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "favorite",
                table: "Coins");
        }
    }
}
