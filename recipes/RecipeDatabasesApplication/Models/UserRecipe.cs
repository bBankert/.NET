using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;


namespace RecipeDatabasesApplication.Models
{
    [Table("UserRecipes")]
    public class UserRecipe
    {
        public int UserRecipeID { get; set; }
        public int UserID { get; set; }
        public int RecipeID { get; set; }

        public virtual User Users { get; set; }
        public virtual Recipe Recipes { get; set; }


    }
}