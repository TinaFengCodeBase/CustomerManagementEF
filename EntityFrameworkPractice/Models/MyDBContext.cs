using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkPractice.Models
{
    internal class MyDBContext:DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Detail> Details { get; set; }
        public MyDBContext() : base("MyDBConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyDBContext, Migrations.Configuration>("MyDBConnectionString"));
        }
    }
}
