using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwncmsCore.Migrations
{
    public partial class addprofile_image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "profile_image_file_idid",
                table: "users_tbl",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_users_tbl_profile_image_file_idid",
                table: "users_tbl",
                column: "profile_image_file_idid");

            migrationBuilder.AddForeignKey(
                name: "FK_users_tbl_file_tbl_profile_image_file_idid",
                table: "users_tbl",
                column: "profile_image_file_idid",
                principalTable: "file_tbl",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_tbl_file_tbl_profile_image_file_idid",
                table: "users_tbl");

            migrationBuilder.DropIndex(
                name: "IX_users_tbl_profile_image_file_idid",
                table: "users_tbl");

            migrationBuilder.DropColumn(
                name: "profile_image_file_idid",
                table: "users_tbl");
        }
    }
}
