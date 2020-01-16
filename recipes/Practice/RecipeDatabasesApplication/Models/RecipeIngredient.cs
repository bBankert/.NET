using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeDatabasesApplication.Models
{
    [Table("RecipeIngredients")]
    public class RecipeIngredient
    {
        public int RecipeIngredientID { get; set; }
        public int RecipeID { get; set; }
        public int IngredientID { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}