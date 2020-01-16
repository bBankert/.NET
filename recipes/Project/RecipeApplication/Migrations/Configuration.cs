namespace RecipeApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using RecipeApplication.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<RecipeApplication.Data.RecipeBookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(RecipeApplication.Data.RecipeBookContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var users = new List<User>
            {
                new User{Name="Tom",Age=22,Gender=Gender.Male,Username="t123",Password="abc"},
                new User{Name="Abbie",Age=20,Gender=Gender.Female,Username="a123",Password="def"},
                new User{Name="Hank",Age=18,Gender=Gender.Male,Username="h123",Password="ghi"},
                new User{Name="Shanon",Age=40,Gender=Gender.Female,Username="s123",Password="jkl"},
                new User{Name="Janine",Age=31,Gender=Gender.Female,Username="j123",Password="mno"}
            };
            users.ForEach(u => context.Users.AddOrUpdate(a => a.Name, u));
            context.SaveChanges();
            //seed recipes
            var recipes = new List<Recipe>
            {
                new Recipe{RecipeName="Pizza"},
                new Recipe{RecipeName="Hot Dog"},
                new Recipe{RecipeName="Turkey"},
                new Recipe{RecipeName="Cereal"},
                new Recipe{RecipeName="Coffee"}
            };
            recipes.ForEach(r => context.Recipes.AddOrUpdate(a => a.RecipeName, r));
            context.SaveChanges();
            //seed ingredients
            var ingredients = new List<Ingredient>
            {
                new Ingredient{IngredientName="Flour"},
                new Ingredient{IngredientName="Sugar"},
                new Ingredient{IngredientName="Milk"},
                new Ingredient{IngredientName="Salt"},
                new Ingredient{IngredientName="Cheese"},
            };
            ingredients.ForEach(i => context.Ingredients.AddOrUpdate(a => a.IngredientName, i));
            context.SaveChanges();
            //seed userRecipes *IMPORTANT for seeding many-to-many
            var userRecipes = new List<UserRecipe>
            {
                new UserRecipe
                {
                    UserId = users.Single(u => u.Name == "Tom").UserId,
                    RecipeId = recipes.Single(r => r.RecipeName == "Pizza").RecipeId
                },
                new UserRecipe
                {
                    UserId = users.Single(u => u.Name == "Tom").UserId,
                    RecipeId = recipes.Single(r => r.RecipeName == "Turkey").RecipeId
                },
                new UserRecipe
                {
                    UserId = users.Single(u => u.Name == "Tom").UserId,
                    RecipeId = recipes.Single(r => r.RecipeName == "Cereal").RecipeId
                },
                new UserRecipe
                {
                    UserId = users.Single(u => u.Name == "Shanon").UserId,
                    RecipeId = recipes.Single(r => r.RecipeName == "Coffee").RecipeId
                },
                new UserRecipe
                {
                    UserId = users.Single(u => u.Name == "Janine").UserId,
                    RecipeId = recipes.Single(r => r.RecipeName == "Hot Dog").RecipeId
                },
                new UserRecipe
                {
                    UserId = users.Single(u => u.Name == "Janine").UserId,
                    RecipeId = recipes.Single(r => r.RecipeName == "Pizza").RecipeId
                },
            };
            foreach (UserRecipe ur in userRecipes)
            {
                var userRecipeInDatabase = context.UserRecipes.Where(
                    a =>
                        a.RecipeId == ur.RecipeId &&
                        a.UserId == ur.UserId).SingleOrDefault();
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
                    RecipeId = recipes.Single(r => r.RecipeName == "Pizza").RecipeId,
                    IngredientId = ingredients.Single(i => i.IngredientName == "Flour").IngredientId
                },
                new RecipeIngredient
                {
                    RecipeId = recipes.Single(r => r.RecipeName == "Pizza").RecipeId,
                    IngredientId = ingredients.Single(i => i.IngredientName == "Sugar").IngredientId
                },
                new RecipeIngredient
                {
                    RecipeId = recipes.Single(r => r.RecipeName == "Pizza").RecipeId,
                    IngredientId = ingredients.Single(i => i.IngredientName == "Milk").IngredientId
                },
                new RecipeIngredient
                {
                    RecipeId = recipes.Single(r => r.RecipeName == "Pizza").RecipeId,
                    IngredientId = ingredients.Single(i => i.IngredientName == "Salt").IngredientId
                },
                new RecipeIngredient
                {
                    RecipeId = recipes.Single(r => r.RecipeName == "Pizza").RecipeId,
                    IngredientId = ingredients.Single(i => i.IngredientName == "Cheese").IngredientId
                },
                //Turkey
                new RecipeIngredient
                {
                    RecipeId = recipes.Single(r => r.RecipeName == "Turkey").RecipeId,
                    IngredientId = ingredients.Single(i => i.IngredientName == "Salt").IngredientId
                },
                //Hot Dog
                new RecipeIngredient
                {
                    RecipeId = recipes.Single(r => r.RecipeName == "Hot Dog").RecipeId,
                    IngredientId = ingredients.Single(i => i.IngredientName == "Salt").IngredientId
                },
                //Cereal
                new RecipeIngredient
                {
                    RecipeId = recipes.Single(r => r.RecipeName == "Cereal").RecipeId,
                    IngredientId = ingredients.Single(i => i.IngredientName == "Flour").IngredientId
                },
                new RecipeIngredient
                {
                    RecipeId = recipes.Single(r => r.RecipeName == "Cereal").RecipeId,
                    IngredientId = ingredients.Single(i => i.IngredientName == "Sugar").IngredientId
                },
                new RecipeIngredient
                {
                    RecipeId = recipes.Single(r => r.RecipeName == "Cereal").RecipeId,
                    IngredientId = ingredients.Single(i => i.IngredientName == "Milk").IngredientId
                },
                new RecipeIngredient
                {
                    RecipeId = recipes.Single(r => r.RecipeName == "Cereal").RecipeId,
                    IngredientId = ingredients.Single(i => i.IngredientName == "Salt").IngredientId
                },
                //Coffee
                new RecipeIngredient
                {
                    RecipeId = recipes.Single(r => r.RecipeName == "Coffee").RecipeId,
                    IngredientId = ingredients.Single(i => i.IngredientName == "Sugar").IngredientId
                },
                new RecipeIngredient
                {
                    RecipeId = recipes.Single(r => r.RecipeName == "Coffee").RecipeId,
                    IngredientId = ingredients.Single(i => i.IngredientName == "Milk").IngredientId
                },
            };
            foreach (RecipeIngredient ri in recipeIngredients)
            {
                var recipeIngredientInDatabase = context.RecipeIngredients.Where(
                    a =>
                        a.RecipeId == ri.RecipeId &&
                        a.IngredientId == ri.IngredientId).SingleOrDefault();
                if (recipeIngredientInDatabase == null)
                {
                    context.RecipeIngredients.Add(ri);
                }
            }
            context.SaveChanges();


        }
    }
}
