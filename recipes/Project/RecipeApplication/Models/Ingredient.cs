using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace RecipeApplication.Models
{
    [Table("Ingredients")]
    public class Ingredient
    {
        public int IngredientId { get; set; }


        [Required,MinLength(1),Display(Name ="Ingredient Name")]
        public string IngredientName { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }

    }
}