using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.DatabaseModels
{
    public class Database_DB:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rezafta\Desktop\OwncmsCore\OwncmsCore\DB\DB.mdf;Integrated Security=True;Connect Timeout=30");
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-UFBPQJ8;Initial Catalog=OwnCmsCore;Integrated Security=True");
        }

        public DbSet<settings_tbl> settings_tbl { get; set; }
        public DbSet<city_tbls> city_tbls { get; set; }
        public DbSet<file_tbl> file_tbl { get; set; }
        public DbSet<users_tbl> users_tbl { get; set; }
        public DbSet<userstype_tbl> userstype_tbl { get; set; }
        public DbSet<user_verify_tbl> user_verify_tbl { get; set; }
        public DbSet<notifications_tbl> notifications_tbl { get; set; }
        public DbSet<categories_tbl> categories_tbl { get; set; }
        public DbSet<posts_tbls> posts_tbls { get; set; }
        public DbSet<report_tbl> report_tbl { get; set; }
        public DbSet<reports_tbl> reports_tbl { get; set; }
        public DbSet<store_categories_tbl> store_categories_tbl { get; set; }
        public DbSet<store_tbl> store_tbl { get; set; }
        public DbSet<store_images_tbls> store_images_tbls { get; set; }
        public DbSet<discount_code_tbl> discount_code_tbl { get; set; }
        public DbSet<orders_tbls> orders_tbls { get; set; }
        public DbSet<transcation_tbl> transcation_tbl { get; set; }


    }
}