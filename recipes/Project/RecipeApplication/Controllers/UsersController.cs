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
//using RecipeApplication.Controllers;

namespace RecipeApplication.Controllers
{
    
    public class UsersController : Controller
    {
        private RecipeBookContext db = new RecipeBookContext();
       

        //check for unique username
        public JsonResult UserExists(string username)
        {
            return Json(!db.Users.Any(u => u.Username == username), JsonRequestBehavior.AllowGet);
        }

        // GET: Users
        
        public ActionResult Index()
        {
            string username = this.Session["Username"] as string;
            //System.Diagnostics.Debug.WriteLine(user.Name);
            if (username == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var loggedInUser = db.Users.Single(u => u.Username.Equals(username));
            

            ViewBag.NameOfUser = loggedInUser.Name;
            


            IQueryable < RecipeGroup > recipes =
                from user in db.Users
                join userRecipe in db.UserRecipes on user.UserId equals userRecipe.UserId
                join recipe in db.Recipes on userRecipe.RecipeId equals recipe.RecipeId
                where loggedInUser.UserId == user.UserId
                select new RecipeGroup()
                {
                    RecipeName = recipe.RecipeName,
                    RecipeId = recipe.RecipeId,
                    
                };

            IList<RecipeGroup> finalRecipes = new List<RecipeGroup>();
                
            foreach(var recipe in recipes)
            {
                var ingredients =
                    from r in db.Recipes
                    join ri in db.RecipeIngredients on r.RecipeId equals ri.RecipeId
                    join i in db.Ingredients on ri.IngredientId equals i.IngredientId
                    where recipe.RecipeId == r.RecipeId
                    select i.IngredientCost;
                decimal cost;
                try
                {
                    cost = ingredients.Sum();
                }
                catch (InvalidOperationException)
                {
                    //System.Diagnostics.Debug.WriteLine(recipe.RecipeName);
                    cost = 0.00M;
                }
                
                finalRecipes.Add(new RecipeGroup
                {
                    RecipeName = recipe.RecipeName,
                    RecipeId = recipe.RecipeId,
                    RecipeCost = cost
                });
                
                //System.Diagnostics.Debug.WriteLine(cost);
            }



            return View(finalRecipes.ToList());
            
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Name,Age,Gender,Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit()
        {
            string username = this.Session["Username"] as string;
            
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Users");
            }
            int id = db.Users.Where(u => u.Username.Equals(username)).Single().UserId;
            
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Name,Age,Gender,Username,Password")] User user)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Users");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
