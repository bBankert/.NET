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
    }
}