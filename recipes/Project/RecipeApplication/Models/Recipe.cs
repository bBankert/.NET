using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace RecipeApplication.Models
{
    [Table("Recipes")]
    public class Recipe
    {
        public int RecipeId { get; set; }

        [Required,MinLength(1),Display(Name ="Name")]
        public string RecipeName { get; set; }

        public virtual ICollection<UserRecipe> UserRecipes { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}