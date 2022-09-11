using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwncmsCore.Migrations
{
    public partial class remove_peyment_day : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "peyment_day_count",
                table: "Users_Tbl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "peyment_day_count",
                table: "Users_Tbl",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
