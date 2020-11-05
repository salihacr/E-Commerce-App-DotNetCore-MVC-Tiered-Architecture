using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Commerce.DataAccess.Migrations
{
    public partial class ishomeadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHome",
                table: "Products",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHome",
                table: "Products");
        }
    }
}
