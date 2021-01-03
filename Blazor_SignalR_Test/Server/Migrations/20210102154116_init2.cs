using Microsoft.EntityFrameworkCore.Migrations;

namespace Blazor_SignalR_Test.Server.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Coins",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Coins");
        }
    }
}
