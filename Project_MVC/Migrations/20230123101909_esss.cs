using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_MVC.Migrations
{
    public partial class esss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataDodania",
                table: "Products",
                newName: "AddData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddData",
                table: "Products",
                newName: "DataDodania");
        }
    }
}
