using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Association table for user and recipe
/// </summary>
namespace RecipeApplication.Models
{
    public class UserRecipe
    {
        public int UserRecipeId { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}