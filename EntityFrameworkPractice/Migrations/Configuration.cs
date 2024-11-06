namespace EntityFrameworkPractice.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using EntityFrameworkPractice.Models;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityFrameworkPractice.Models.MyDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EntityFrameworkPractice.Models.MyDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            IList<Detail> defaultDetails = new List<Detail>();

            defaultDetails.Add(new Detail() { FirstName = "Tina", LastName = "Feng", Age = 29, Address = "1234 Main Street", DOB  = DateTime.Now });

            defaultDetails.Add(new Detail() { FirstName = "Henry", LastName = "Smith", Age = 39, Address = "567 Main Street", DOB = DateTime.Now });

            defaultDetails.Add(new Detail() { FirstName = "Gary", LastName = "Anderson", Age = 11, Address = "890 Main Street", DOB = DateTime.Now });

            foreach (Detail detail in defaultDetails)
            {
                context.Details.Add(detail);
            }
           
            base.Seed(context);
        }
    }
}
