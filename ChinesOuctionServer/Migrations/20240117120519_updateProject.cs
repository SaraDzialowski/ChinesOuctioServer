using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChinesOuctionServer.Migrations
{
    public partial class updateProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quentity",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quentity",
                table: "OrderItems");
        }
    }
}
