using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HelpdeskTickets.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        public int TicketId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public Progress Progress { get; set; } = Progress.NotStarted;

        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public int OwnerId { get; set; }
        public virtual User Owner { get; set; }
    }

    public enum Progress
    {
        [Display(Name ="Not Started")]
        NotStarted = 1,
        [Display(Name ="In Progress")]
        InProgress = 2,
        [Display(Name ="Complete")]
        Complete = 3
    }
}