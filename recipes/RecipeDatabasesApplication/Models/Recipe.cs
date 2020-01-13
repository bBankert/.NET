using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeDatabasesApplication.Models
{
    [Table("Reicpes")]
    public class Recipe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecipeID { get; set; }

        [Required, MinLength(1)]
        public string Name { get; set; }


        public virtual ICollection<UserRecipe> UserRecipes { get; set; }
    }
}