using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HelpdeskTickets.Models;
using System.Data.Entity;

namespace HelpdeskTickets.Data
{
    public class SystemContext : DbContext
    {
        public SystemContext() : base("SystemContext")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOptional(o => o.Owner)
                .WithMany(u => u.TicketsOwned);
            modelBuilder.Entity<Ticket>()
                .HasRequired(c => c.Creator)
                .WithMany(u => u.TicketsCreated);

            base.OnModelCreating(modelBuilder);
        }
    }
}