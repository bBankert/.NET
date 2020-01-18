using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecipeApplication.Controllers;

namespace RecipeApplication.Models
{
    [Table("Users")]
    public class User
    {
        public int UserId { get; set; }

        [Required,Display(Name="Name"),MinLength(1)]
        public string Name { get; set; }
        [Required,Range(0,125)]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [MinLength(1),Required,Remote("UserExists","Users",ErrorMessage ="Username in use")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual ICollection<UserRecipe> UserRecipes { get; set; }


    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}