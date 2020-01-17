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
        public ActionResult Create(string recipeName,string username)
        {
            //ViewBag.RecipeId = db.Recipes.Where(r => r.RecipeName.Equals(recipeName)).Single().RecipeId;

            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IngredientId,IngredientName")] Ingredient ingredient)
        {
            string recipe = Request.QueryString["recipeName"];
            //add recipeIngredient link for association table
            int recipeId = db.Recipes.Where(r => r.RecipeName.Equals(recipe)).Single().RecipeId;
            if (ModelState.IsValid)
            {
                bool ingredientExists = db.Ingredients.Where(i => i.IngredientName.Equals(ingredient.IngredientName)).Count() == 1 ? true : false;
                RecipeIngredient recipeIngredient = new RecipeIngredient()
                {
                    RecipeId = recipeId,
                    IngredientId = ingredient.IngredientId
                };
                if (!ingredientExists)
                {
                    db.Ingredients.Add(ingredient);
                    
                }
                db.RecipeIngredients.Add(recipeIngredient);

                db.SaveChanges();
                return RedirectToAction("Index","Users",new { username = Request.QueryString["username"] });
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
        public ActionResult Edit([Bind(Include = "IngredientId,IngredientName")] Ingredient ingredient)
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
        public ActionResult Delete(int? id)
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
            Ingredient ingredient = db.Ingredients.Find(id);
            db.Ingredients.Remove(ingredient);
            db.SaveChanges();
            return RedirectToAction("Index");
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
