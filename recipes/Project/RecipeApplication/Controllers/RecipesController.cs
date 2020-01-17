using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecipeApplication.Data;
using RecipeApplication.Models;
using RecipeApplication.ViewModels;

namespace RecipeApplication.Controllers
{
    public class RecipesController : Controller
    {
        private RecipeBookContext db = new RecipeBookContext();

        //make unique recipe name
        public JsonResult RecipeExists(string recipename)
        {
            return Json(!db.Recipes.Any(u => u.RecipeName == recipename), JsonRequestBehavior.AllowGet);
        }



        // GET: Recipes/Details/5
        public ActionResult Details(int? id, string username)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Recipe recipe = db.Recipes.Find(id);
            ViewBag.Username = username;
            ViewBag.Recipe = recipe.RecipeName;
            if (recipe == null)
            {
                return HttpNotFound();
            }

            IQueryable<IngredientGroup> ingredients =
                from r in db.Recipes
                join recipeIngredient in db.RecipeIngredients on r.RecipeId equals recipeIngredient.RecipeId
                join ingredient in db.Ingredients on recipeIngredient.IngredientId equals ingredient.IngredientId
                where recipe.RecipeId == r.RecipeId
                select new IngredientGroup()
                {
                    IngredientId = ingredient.IngredientId,
                    IngredientName = ingredient.IngredientName
                };


            return View(ingredients.ToList());
        }

        // GET: Recipes/Create
        public ActionResult Create(string username)
        {
            ViewBag.Username = username;
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecipeId,RecipeName")] Recipe recipe)
        {
            string user = Request.QueryString["username"];
            if (ModelState.IsValid)
            {
                int userId = db.Users.Where(u => u.Username.Equals(user)).Single().UserId;
                
               
                bool recipeExists = db.Recipes.Where(r => r.RecipeName.Equals(recipe.RecipeName)).Count() == 1 ? true : false;
                if (!recipeExists)
                {
                    //System.Diagnostics.Debug.WriteLine("Doesn't exist");
                    db.Recipes.Add(recipe);
                    db.SaveChanges();
                }

                UserRecipe userRecipe = new UserRecipe()
                {
                    UserId = db.Users.Single(u => u.Username.Equals(user)).UserId,
                    RecipeId = db.Recipes.Single(r => r.RecipeName.Equals(recipe.RecipeName)).RecipeId
                };

                db.UserRecipes.Add(userRecipe);

                db.SaveChanges();
                return RedirectToAction("Index","Users", new { username = user });
            }

            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipeId,RecipeName")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public ActionResult Delete(int? id,string username)
        {
            ViewBag.Username = username;
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Index", "Home");
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string username = Request.QueryString["username"];
            Recipe recipe = db.Recipes.Find(id);
            
            //check associations and remove the link from the user to the recipe
            int UserId = db.Users.Where(u => u.Username.Equals(username)).Single().UserId;
            UserRecipe userRecipe = db.UserRecipes.Where(ur => ur.UserId.Equals(UserId) && ur.RecipeId.Equals(id)).Single();
            //only delete the recipe if this was the last/only link
            int linkCount = db.UserRecipes.Where(c => c.RecipeId.Equals(id)).Count();
            
            if (linkCount <= 1)
            {
                db.Recipes.Remove(recipe);
                
            }
            db.UserRecipes.Remove(userRecipe);
            db.SaveChanges();
            return RedirectToAction("Index","Users",new { username = username });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
