using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodDonationApp.Data.Migrations
{
    public partial class addnewfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Centers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Centers");
        }
    }
}
