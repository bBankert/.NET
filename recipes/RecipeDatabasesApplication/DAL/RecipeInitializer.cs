using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RecipeDatabasesApplication.Models;

namespace RecipeDatabasesApplication.DAL
{
    public class RecipeInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<RecipeDBContext>
    {
        protected override void Seed(RecipeDBContext context)
        {
            //seed users
            var users = new List<User>
            {
                new User{Name="Tom",Username="t123",Password="abc"},
                new User{Name="Abbie",Username="a123",Password="def"},
                new User{Name="Hank",Username="h123",Password="ghi"},
                new User{Name="Shanon",Username="s123",Password="jkl"},
                new User{Name="Janine",Username="j123",Password="mno"}
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
            //seed recipes
            var recipes = new List<Recipe>
            {
                new Recipe{RecipeID=1001,Name="Pizza"},
                new Recipe{RecipeID=1002,Name="Hot Dog"},
                new Recipe{RecipeID=1003,Name="Turkey"},
                new Recipe{RecipeID=1004,Name="Cereal"},
                new Recipe{RecipeID=1005,Name="Coffee"}
            };
            recipes.ForEach(r => context.Recipes.Add(r));
            context.SaveChanges();
            //seed ingredients
            var ingredients = new List<Ingredient>
            {
                new Ingredient{Name="Flour"},
                new Ingredient{Name="Sugar"},
                new Ingredient{Name="Milk"},
                new Ingredient{Name="Salt"},
                new Ingredient{Name="Cheese"},
            };
            ingredients.ForEach(i => context.Ingredients.Add(i));
            context.SaveChanges();
            //seed user recipes
            var userRecipes = new List<UserRecipe>
            {
                new UserRecipe{UserID=1,RecipeID=1005},
                new UserRecipe{UserID=1,RecipeID=1003},
                new UserRecipe{UserID=2,RecipeID=1004},
                new UserRecipe{UserID=3,RecipeID=1003},
                new UserRecipe{UserID=4,RecipeID=1002},
                new UserRecipe{UserID=5,RecipeID=1001}
            };
            userRecipes.ForEach(u => context.UserRecipes.Add(u));
            context.SaveChanges();

        }

    }
}