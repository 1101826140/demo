using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.DBContext
{
    public class WebApiDbcontext : DbContext
    {
        public WebApiDbcontext() : base("DefaultConnection")
        {
            Database.SetInitializer<WebApiDbcontext>(new MigrateDatabaseToLatestVersion<WebApiDbcontext, Configuration<WebApiDbcontext>>());
        }       

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Book> Books { get; set; }

        //public DbSet<TestTable> TestTables { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            //在不声明model中的table名称时，系统默认分配表名为复数
            //该代码是取消表名的命名，注意最好是在库刚建时写写，后面表建好后 再写该代码会报错
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            //modelBuilder.Entity<Product>().ToTable("Product").HasKey(p=>new { })

        }
         
    }


    public class Configuration<T> : DbMigrationsConfiguration<T> where T : DbContext
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}