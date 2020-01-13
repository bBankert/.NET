using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeDatabasesApplication.Models
{
    [Table("Ingredients")]
    public class Ingredient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IngredientID { get; set; }

        [Required, MinLength(1)]
        public string Name { get; set; }

        //public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}