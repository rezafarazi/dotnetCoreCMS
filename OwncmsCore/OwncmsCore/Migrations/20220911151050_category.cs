using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwncmsCore.Migrations
{
    public partial class category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city_id",
                table: "Categories_Tbl");

            migrationBuilder.DropColumn(
                name: "two_way",
                table: "Categories_Tbl");

            migrationBuilder.RenameColumn(
                name: "header_image",
                table: "Categories_Tbl",
                newName: "link");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "link",
                table: "Categories_Tbl",
                newName: "header_image");

            migrationBuilder.AddColumn<long>(
                name: "city_id",
                table: "Categories_Tbl",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "two_way",
                table: "Categories_Tbl",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
