using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RecipeDatabasesApplication.Models
{
    [Table("Users")]
    public class User
    {
        public int UserID { get; set; }

        [Required,MinLength(1)]
        public string Name { get; set; }

        [Required,MinLength(1),Display(Name="Username")]
        public string Username { get; set; }

        [Required,MinLength(1),Display(Name ="Password")]
        public string Password { get; set; }


        public virtual ICollection<UserRecipe> UserRecipes { get; set; }

    }
}