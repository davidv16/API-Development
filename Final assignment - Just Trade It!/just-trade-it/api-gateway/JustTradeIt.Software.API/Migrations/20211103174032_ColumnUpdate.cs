using Microsoft.EntityFrameworkCore.Migrations;

namespace JustTradeIt.Software.API.Migrations
{
    public partial class ColumnUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TradeStatus",
                table: "Trades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TradeStatus",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "Items");
        }
    }
}
