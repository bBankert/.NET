namespace HelpdeskTickets.Migrations
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using HelpdeskTickets.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<HelpdeskTickets.Data.SystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HelpdeskTickets.Data.SystemContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var users = new List<User>
            {
                new User{Name="Brandon",Username="bxb",Password="iceCream",Permission=Permission.Staff},
                new User{Name="Tom",Username="t123",Password="abc",Permission=Permission.Client},
                new User{Name="Shannon",Username="sh01",Password="p@ss",Permission=Permission.Client},
                new User{Name="Admin",Username="admin",Password="password",Permission=Permission.Staff},
                new User{Name="Janine",Username="j123",Password="mno",Permission=Permission.Client}
            };
            users.ForEach(u => context.Users.AddOrUpdate(a => a.Name, u));
            context.SaveChanges();

            
            

        }
    }
}
