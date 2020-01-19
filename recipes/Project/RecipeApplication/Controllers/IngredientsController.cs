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

namespace RecipeApplication.Controllers
{
    public class IngredientsController : Controller
    {
        private RecipeBookContext db = new RecipeBookContext();

        public JsonResult IngredientExists(string ingredient)
        {
            return Json(!db.Ingredients.Any(u => u.IngredientName == ingredient), JsonRequestBehavior.AllowGet);
        }

        // GET: Ingredients/Create
        public ActionResult Create()
        {
            string recipeName = this.Session["RecipeName"] as string;
            string username = this.Session["Username"] as string;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(recipeName))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IngredientId,IngredientName,IngredientCost")] Ingredient ingredient)
        {
            string recipe = this.Session["RecipeName"] as string;
            //add recipeIngredient link for association table
            int recipeId = db.Recipes.Where(r => r.RecipeName.Equals(recipe)).Single().RecipeId;
            if (ModelState.IsValid)
            {
                bool ingredientExists = db.Ingredients.Where(i => i.IngredientName.Equals(ingredient.IngredientName)).Count() == 1 ? true : false;
                
                if (!ingredientExists)
                {
                    db.Ingredients.Add(ingredient);
                    db.SaveChanges();
                }
                RecipeIngredient recipeIngredient = new RecipeIngredient()
                {
                    RecipeId = db.Recipes.Single(r => r.RecipeName.Equals(recipe)).RecipeId,
                    IngredientId = db.Ingredients.Single(r => r.IngredientName.Equals(ingredient.IngredientName)).IngredientId
                };

                db.RecipeIngredients.Add(recipeIngredient);

                db.SaveChanges();
                return RedirectToAction("Details","Recipes",new {id = recipeId});
            }

            return View(ingredient);
        }

        // GET: Ingredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IngredientId,IngredientName,IngredientCost")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ingredient);
        }

        // GET: Ingredients/Delete/5
        public ActionResult Delete(int? id,string username,string recipeName)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string recipe = this.Session["RecipeName"] as string;

            int recipeId = db.Recipes.Where(r => r.RecipeName.Equals(recipe)).Single().RecipeId;
            int linkCount = db.RecipeIngredients.Where(ri => ri.IngredientId.Equals(id)).Count();
            System.Diagnostics.Debug.WriteLine(linkCount);
            RecipeIngredient recipeIngredient = db.RecipeIngredients.Single(ri => ri.IngredientId.Equals(id) && ri.RecipeId.Equals(recipeId));
            if (linkCount == 1)
            {
                Ingredient ingredient = db.Ingredients.Find(id);
                db.Ingredients.Remove(ingredient);
            }
            db.RecipeIngredients.Remove(recipeIngredient);
            db.SaveChanges();
            string username = this.Session["Username"] as string;

            return RedirectToAction("Details", "Recipes", new { id = recipeId});
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
