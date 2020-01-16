using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RecipeDatabasesApplication.ViewModels
{
    public class RecipeGroup
    {
        public string Recipe { get; set; }

        [Display(Name ="Number of Users")]
        public int UserCount { get; set; }
        /*[Display(Name ="Number of Ingredients")]
        public int IngredientCount { get; set; }*/
    }
}