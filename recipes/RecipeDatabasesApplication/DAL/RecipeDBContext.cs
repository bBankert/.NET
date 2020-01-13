using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RecipeDatabasesApplication.Models;

namespace RecipeDatabasesApplication.DAL
{
    public class RecipeDBContext : DbContext
    {
        public RecipeDBContext() : base("RecipeDBContext")
        {
            
        }
        /*public void initializeDatabase(RecipeDBContext context)
        {
            if (context.Database.Exists())
            {
                if (!context.Database.CompatibleWithModel(true))
                {
                    context.Database.Delete();
                }
            }
            context.Database.Create();
        }*/
        public DbSet<UserRecipe> UserRecipes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        //public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        
    }
}