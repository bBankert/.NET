using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeDatabasesApplication.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }

        [Required, MinLength(1)]
        public string Name { get; set; }

        //public virtual ICollection<Recipe> Recipes { get; set; }
    }
}