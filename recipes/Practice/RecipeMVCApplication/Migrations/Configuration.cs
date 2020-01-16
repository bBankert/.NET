namespace RecipeApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using RecipeApplication.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<RecipeApplication.Models.RecipeDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RecipeApplication.Models.RecipeDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //initialization insertion
            context.Recipes.AddOrUpdate(i => i.Name,
                new Recipe
                {
                    Name = "Hamburger",
                    Date = DateTime.Parse("1/01/2020")
                },
                new Recipe
                {
                    Name = "Salad",
                    Date = DateTime.Parse("1/02/2020")
                },
                new Recipe
                {
                    Name = "Pizza",
                    Date = DateTime.Parse("1/03/2020")
                },
                new Recipe
                {
                    Name = "Hotdog",
                    Date = DateTime.Parse("1/04/2020")
                },
                new Recipe
                {
                    Name = "Steak",
                    Date = DateTime.Parse("1/05/2020")
                }

            );

        }
    }
}
