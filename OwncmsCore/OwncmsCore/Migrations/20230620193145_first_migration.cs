using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwncmsCore.Migrations
{
    public partial class first_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories_tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    root_cat_id = table.Column<int>(type: "int", nullable: false),
                    icon_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datetime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    castom_link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    visible = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    delete_flag = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories_tbl", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "city_tbls",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    area = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datetime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city_tbls", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "discount_code_tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    delete_flag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discount_code_tbl", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "file_tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    file_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_address_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_extention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_date_time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_tbl", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "report_tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    delete_flag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_tbl", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "settings_tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parametrs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    discountall = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settings_tbl", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "store_categories_tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    root_cat_id = table.Column<int>(type: "int", nullable: false),
                    icon_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datetime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    castom_link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    visible = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    delete_falg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_store_categories_tbl", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "userstype_tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    en_title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userstype_tbl", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users_tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    family = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    login_token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date_time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    google_auth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city_idid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_tbl", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_tbl_city_tbls_city_idid",
                        column: x => x.city_idid,
                        principalTable: "city_tbls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications_tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    from_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    seen = table.Column<bool>(type: "bit", nullable: false),
                    user_idid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications_tbl", x => x.id);
                    table.ForeignKey(
                        name: "FK_notifications_tbl_users_tbl_user_idid",
                        column: x => x.user_idid,
                        principalTable: "users_tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "posts_tbls",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    delete_flag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    visible = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_idid = table.Column<long>(type: "bigint", nullable: false),
                    category_idid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts_tbls", x => x.id);
                    table.ForeignKey(
                        name: "FK_posts_tbls_categories_tbl_category_idid",
                        column: x => x.category_idid,
                        principalTable: "categories_tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_posts_tbls_users_tbl_user_idid",
                        column: x => x.user_idid,
                        principalTable: "users_tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "store_tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    delete_flag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    visible = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    json = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    discount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adiscount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_idid = table.Column<long>(type: "bigint", nullable: false),
                    category_idid = table.Column<long>(type: "bigint", nullable: false),
                    file_idid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_store_tbl", x => x.id);
                    table.ForeignKey(
                        name: "FK_store_tbl_file_tbl_file_idid",
                        column: x => x.file_idid,
                        principalTable: "file_tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_store_tbl_store_categories_tbl_category_idid",
                        column: x => x.category_idid,
                        principalTable: "store_categories_tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_store_tbl_users_tbl_user_idid",
                        column: x => x.user_idid,
                        principalTable: "users_tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_verify_tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hash_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_idid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_verify_tbl", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_verify_tbl_users_tbl_user_idid",
                        column: x => x.user_idid,
                        principalTable: "users_tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reports_tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date_time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    condition = table.Column<bool>(type: "bit", nullable: false),
                    report_idid = table.Column<long>(type: "bigint", nullable: false),
                    user_idid = table.Column<long>(type: "bigint", nullable: false),
                    post_idid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports_tbl", x => x.id);
                    table.ForeignKey(
                        name: "FK_reports_tbl_posts_tbls_post_idid",
                        column: x => x.post_idid,
                        principalTable: "posts_tbls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reports_tbl_report_tbl_report_idid",
                        column: x => x.report_idid,
                        principalTable: "report_tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reports_tbl_users_tbl_user_idid",
                        column: x => x.user_idid,
                        principalTable: "users_tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "orders_tbls",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    store_idid = table.Column<long>(type: "bigint", nullable: false),
                    user_idid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders_tbls", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_tbls_store_tbl_store_idid",
                        column: x => x.store_idid,
                        principalTable: "store_tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_tbls_users_tbl_user_idid",
                        column: x => x.user_idid,
                        principalTable: "users_tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "store_images_tbls",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    store_idid = table.Column<long>(type: "bigint", nullable: false),
                    file_idid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_store_images_tbls", x => x.id);
                    table.ForeignKey(
                        name: "FK_store_images_tbls_file_tbl_file_idid",
                        column: x => x.file_idid,
                        principalTable: "file_tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_store_images_tbls_store_tbl_store_idid",
                        column: x => x.store_idid,
                        principalTable: "store_tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "transcation_tbl",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_idid = table.Column<long>(type: "bigint", nullable: false),
                    order_idid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transcation_tbl", x => x.id);
                    table.ForeignKey(
                        name: "FK_transcation_tbl_orders_tbls_order_idid",
                        column: x => x.order_idid,
                        principalTable: "orders_tbls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transcation_tbl_users_tbl_user_idid",
                        column: x => x.user_idid,
                        principalTable: "users_tbl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_notifications_tbl_user_idid",
                table: "notifications_tbl",
                column: "user_idid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_tbls_store_idid",
                table: "orders_tbls",
                column: "store_idid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_tbls_user_idid",
                table: "orders_tbls",
                column: "user_idid");

            migrationBuilder.CreateIndex(
                name: "IX_posts_tbls_category_idid",
                table: "posts_tbls",
                column: "category_idid");

            migrationBuilder.CreateIndex(
                name: "IX_posts_tbls_user_idid",
                table: "posts_tbls",
                column: "user_idid");

            migrationBuilder.CreateIndex(
                name: "IX_reports_tbl_post_idid",
                table: "reports_tbl",
                column: "post_idid");

            migrationBuilder.CreateIndex(
                name: "IX_reports_tbl_report_idid",
                table: "reports_tbl",
                column: "report_idid");

            migrationBuilder.CreateIndex(
                name: "IX_reports_tbl_user_idid",
                table: "reports_tbl",
                column: "user_idid");

            migrationBuilder.CreateIndex(
                name: "IX_store_images_tbls_file_idid",
                table: "store_images_tbls",
                column: "file_idid");

            migrationBuilder.CreateIndex(
                name: "IX_store_images_tbls_store_idid",
                table: "store_images_tbls",
                column: "store_idid");

            migrationBuilder.CreateIndex(
                name: "IX_store_tbl_category_idid",
                table: "store_tbl",
                column: "category_idid");

            migrationBuilder.CreateIndex(
                name: "IX_store_tbl_file_idid",
                table: "store_tbl",
                column: "file_idid");

            migrationBuilder.CreateIndex(
                name: "IX_store_tbl_user_idid",
                table: "store_tbl",
                column: "user_idid");

            migrationBuilder.CreateIndex(
                name: "IX_transcation_tbl_order_idid",
                table: "transcation_tbl",
                column: "order_idid");

            migrationBuilder.CreateIndex(
                name: "IX_transcation_tbl_user_idid",
                table: "transcation_tbl",
                column: "user_idid");

            migrationBuilder.CreateIndex(
                name: "IX_user_verify_tbl_user_idid",
                table: "user_verify_tbl",
                column: "user_idid");

            migrationBuilder.CreateIndex(
                name: "IX_users_tbl_city_idid",
                table: "users_tbl",
                column: "city_idid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discount_code_tbl");

            migrationBuilder.DropTable(
                name: "notifications_tbl");

            migrationBuilder.DropTable(
                name: "reports_tbl");

            migrationBuilder.DropTable(
                name: "settings_tbl");

            migrationBuilder.DropTable(
                name: "store_images_tbls");

            migrationBuilder.DropTable(
                name: "transcation_tbl");

            migrationBuilder.DropTable(
                name: "user_verify_tbl");

            migrationBuilder.DropTable(
                name: "userstype_tbl");

            migrationBuilder.DropTable(
                name: "posts_tbls");

            migrationBuilder.DropTable(
                name: "report_tbl");

            migrationBuilder.DropTable(
                name: "orders_tbls");

            migrationBuilder.DropTable(
                name: "categories_tbl");

            migrationBuilder.DropTable(
                name: "store_tbl");

            migrationBuilder.DropTable(
                name: "file_tbl");

            migrationBuilder.DropTable(
                name: "store_categories_tbl");

            migrationBuilder.DropTable(
                name: "users_tbl");

            migrationBuilder.DropTable(
                name: "city_tbls");
        }
    }
}
