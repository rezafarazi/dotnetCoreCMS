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
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rezafta\Desktop\OwncmsCore\OwncmsCore\DB\DB.mdf;Integrated Security=True;Connect Timeout=30");
        }

        public DbSet<Categories_Tbl> Categories_Tbl { get; set; }
        public DbSet<Logs_Tbl> Logs_Tbl { get; set; }
        public DbSet<Users_Tbl> Users_Tbl { get; set; }
        public DbSet<Notifications_Tbl> Notifications_Tbl { get; set; }
        public DbSet<Reports_Tbl> Reports_Tbl { get; set; }
        public DbSet<UsersReports_Tbl> UsersReports_Tbl { get; set; }
        public DbSet<Verify_Code_Tbl> Verify_Code_Tbl { get; set; }

    }
}