using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RecipeApplication.ViewModels
{
    public class IngredientGroup
    {

        
        public string IngredientName { get; set; }
        public int IngredientId { get; set; }

        public decimal IngredientCost { get; set; }
    }
}