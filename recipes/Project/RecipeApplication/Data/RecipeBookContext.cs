using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RecipeApplication.Models;

namespace RecipeApplication.Data
{
    public class RecipeBookContext : DbContext
    {
        public RecipeBookContext() : base("RecipeBookContext")
        {

        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<UserRecipe> UserRecipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    }
}