using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwncmsCore.Migrations
{
    public partial class frist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports_Tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports_Tbl", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users_Tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    family = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    login_token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    create_datetime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active_user_flag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    google2f = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    national_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    father_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    profile_image_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    notification = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Tbl", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Categories_Tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enabled_Flag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Root_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Idid = table.Column<long>(type: "bigint", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories_Tbl", x => x.id);
                    table.ForeignKey(
                        name: "FK_Categories_Tbl_Users_Tbl_User_Idid",
                        column: x => x.User_Idid,
                        principalTable: "Users_Tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs_Tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Idid = table.Column<long>(type: "bigint", nullable: false),
                    DateTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs_Tbl", x => x.id);
                    table.ForeignKey(
                        name: "FK_Logs_Tbl_Users_Tbl_User_Idid",
                        column: x => x.User_Idid,
                        principalTable: "Users_Tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications_Tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    from = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    seen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_idid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications_Tbl", x => x.id);
                    table.ForeignKey(
                        name: "FK_Notifications_Tbl_Users_Tbl_user_idid",
                        column: x => x.user_idid,
                        principalTable: "Users_Tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Verify_Code_Tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<long>(type: "bigint", nullable: false),
                    code_hash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verify_Code_Tbl", x => x.id);
                    table.ForeignKey(
                        name: "FK_Verify_Code_Tbl_Users_Tbl_Userid",
                        column: x => x.Userid,
                        principalTable: "Users_Tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts_Tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false),
                    visible = table.Column<bool>(type: "bit", nullable: false),
                    User_Idid = table.Column<long>(type: "bigint", nullable: false),
                    category_idid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts_Tbl", x => x.id);
                    table.ForeignKey(
                        name: "FK_Posts_Tbl_Categories_Tbl_category_idid",
                        column: x => x.category_idid,
                        principalTable: "Categories_Tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Tbl_Users_Tbl_User_Idid",
                        column: x => x.User_Idid,
                        principalTable: "Users_Tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UsersReports_Tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seen = table.Column<bool>(type: "bit", nullable: false),
                    Reporter_Userid = table.Column<long>(type: "bigint", nullable: false),
                    report_postid = table.Column<long>(type: "bigint", nullable: false),
                    Report_Typeid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersReports_Tbl", x => x.id);
                    table.ForeignKey(
                        name: "FK_UsersReports_Tbl_Posts_Tbl_report_postid",
                        column: x => x.report_postid,
                        principalTable: "Posts_Tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersReports_Tbl_Reports_Tbl_Report_Typeid",
                        column: x => x.Report_Typeid,
                        principalTable: "Reports_Tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersReports_Tbl_Users_Tbl_Reporter_Userid",
                        column: x => x.Reporter_Userid,
                        principalTable: "Users_Tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Tbl_User_Idid",
                table: "Categories_Tbl",
                column: "User_Idid");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Tbl_User_Idid",
                table: "Logs_Tbl",
                column: "User_Idid");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Tbl_user_idid",
                table: "Notifications_Tbl",
                column: "user_idid");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Tbl_category_idid",
                table: "Posts_Tbl",
                column: "category_idid");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Tbl_User_Idid",
                table: "Posts_Tbl",
                column: "User_Idid");

            migrationBuilder.CreateIndex(
                name: "IX_UsersReports_Tbl_report_postid",
                table: "UsersReports_Tbl",
                column: "report_postid");

            migrationBuilder.CreateIndex(
                name: "IX_UsersReports_Tbl_Report_Typeid",
                table: "UsersReports_Tbl",
                column: "Report_Typeid");

            migrationBuilder.CreateIndex(
                name: "IX_UsersReports_Tbl_Reporter_Userid",
                table: "UsersReports_Tbl",
                column: "Reporter_Userid");

            migrationBuilder.CreateIndex(
                name: "IX_Verify_Code_Tbl_Userid",
                table: "Verify_Code_Tbl",
                column: "Userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs_Tbl");

            migrationBuilder.DropTable(
                name: "Notifications_Tbl");

            migrationBuilder.DropTable(
                name: "UsersReports_Tbl");

            migrationBuilder.DropTable(
                name: "Verify_Code_Tbl");

            migrationBuilder.DropTable(
                name: "Posts_Tbl");

            migrationBuilder.DropTable(
                name: "Reports_Tbl");

            migrationBuilder.DropTable(
                name: "Categories_Tbl");

            migrationBuilder.DropTable(
                name: "Users_Tbl");
        }
    }
}
