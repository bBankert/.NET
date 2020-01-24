using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace HelpdeskTickets.Models
{
    [Table("Users")]
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, Remote("UserExists", "Users", ErrorMessage = "Username in use")]
        public string Username { get; set; }
        public string Password { get; set; }
        public Permission Permission { get; set; }

        
        public virtual ICollection<Ticket> TicketsCreated { get; set; }
        public virtual ICollection<Ticket> TicketsOwned { get; set; }

    }

    public enum Permission
    {
        Client = 1,
        Staff = 2
    }
}