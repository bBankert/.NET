namespace RecipeDatabasesApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using RecipeDatabasesApplication.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<RecipeDatabasesApplication.DAL.RecipeDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(RecipeDatabasesApplication.DAL.RecipeDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var users = new List<User>
            {
                new User{Name="Tom",Username="t123",Password="abc"},
                new User{Name="Abbie",Username="a123",Password="def"},
                new User{Name="Hank",Username="h123",Password="ghi"},
                new User{Name="Shanon",Username="s123",Password="jkl"},
                new User{Name="Janine",Username="j123",Password="mno"}
            };
            users.ForEach(u => context.Users.AddOrUpdate(a => a.Name, u));
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
            recipes.ForEach(r => context.Recipes.AddOrUpdate(a => a.Name, r));
            context.SaveChanges();
            //seed ingredients
            var ingredients = new List<Ingredient>
            {
                new Ingredient{IngredientID=2001,Name="Flour"},
                new Ingredient{IngredientID=2002,Name="Sugar"},
                new Ingredient{IngredientID=2003,Name="Milk"},
                new Ingredient{IngredientID=2004,Name="Salt"},
                new Ingredient{IngredientID=2005,Name="Cheese"},
            };
            ingredients.ForEach(i => context.Ingredients.AddOrUpdate(a => a.Name, i));
            context.SaveChanges();
            //seed userRecipes *IMPORTANT for seeding many-to-many
            var userRecipes = new List<UserRecipe>
            {
                new UserRecipe
                {
                    UserID = users.Single(u => u.Name == "Tom").UserID,
                    RecipeID = recipes.Single(r => r.Name == "Pizza").RecipeID
                },
                new UserRecipe
                {
                    UserID = users.Single(u => u.Name == "Tom").UserID,
                    RecipeID = recipes.Single(r => r.Name == "Turkey").RecipeID
                },
                new UserRecipe
                {
                    UserID = users.Single(u => u.Name == "Tom").UserID,
                    RecipeID = recipes.Single(r => r.Name == "Cereal").RecipeID
                },
                new UserRecipe
                {
                    UserID = users.Single(u => u.Name == "Shanon").UserID,
                    RecipeID = recipes.Single(r => r.Name == "Coffee").RecipeID
                },
                new UserRecipe
                {
                    UserID = users.Single(u => u.Name == "Janine").UserID,
                    RecipeID = recipes.Single(r => r.Name == "Hot Dog").RecipeID
                },
                new UserRecipe
                {
                    UserID = users.Single(u => u.Name == "Janine").UserID,
                    RecipeID = recipes.Single(r => r.Name == "Pizza").RecipeID
                },
            };
            foreach (UserRecipe ur in userRecipes)
            {
                var userRecipeInDatabase = context.UserRecipes.Where(
                    a =>
                        a.RecipeID == ur.RecipeID &&
                        a.UserID == ur.UserID).SingleOrDefault();
                if (userRecipeInDatabase == null)
                {
                    context.UserRecipes.Add(ur);
                }
            }
            context.SaveChanges();

            var recipeIngredients = new List<RecipeIngredient>
            {
                //Pizza
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Pizza").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Flour").IngredientID
                },
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Pizza").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Sugar").IngredientID
                },
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Pizza").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Milk").IngredientID
                },
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Pizza").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Salt").IngredientID
                },
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Pizza").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Cheese").IngredientID
                },
                //Turkey
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Turkey").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Salt").IngredientID
                },
                //Hot Dog
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Hot Dog").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Salt").IngredientID
                },
                //Cereal
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Cereal").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Flour").IngredientID
                },
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Cereal").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Sugar").IngredientID
                },
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Cereal").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Milk").IngredientID
                },
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Cereal").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Salt").IngredientID
                },
                //Coffee
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Coffee").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Sugar").IngredientID
                },
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Coffee").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Milk").IngredientID
                },
            };
            foreach (RecipeIngredient ri in recipeIngredients)
            {
                var recipeIngredientInDatabase = context.RecipeIngredients.Where(
                    a =>
                        a.RecipeID == ri.RecipeID &&
                        a.IngredientID == ri.IngredientID).SingleOrDefault();
                if (recipeIngredientInDatabase == null)
                {
                    context.RecipeIngredients.Add(ri);
                }
            }
            context.SaveChanges();
        }
    }
}
