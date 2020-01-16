using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Association table for recipe and ingredients
/// </summary>

namespace RecipeApplication.Models
{
    public class RecipeIngredient
    {
        public int RecipeIngredientId { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}