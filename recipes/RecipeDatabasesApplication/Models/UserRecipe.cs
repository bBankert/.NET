using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeDatabasesApplication.Models
{
    public class UserRecipe
    {
        public int UserRecipeID { get; set; }
        public int UserID { get; set; }
        public int RecipeID { get; set; }

        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }


    }
}