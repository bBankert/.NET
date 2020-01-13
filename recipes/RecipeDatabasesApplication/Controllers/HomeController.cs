using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipeDatabasesApplication.DAL;
using RecipeDatabasesApplication.Models;
using RecipeDatabasesApplication.ViewModels;

namespace RecipeDatabasesApplication.Controllers
{
    public class HomeController : Controller
    {
        private RecipeDBContext db = new RecipeDBContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<RecipeGroup> data = from user in db.UserRecipes
                                           group user by user.Recipe.Name into recipeGroup
                                           select new RecipeGroup()
                                           {
                                               Recipe = recipeGroup.Key,
                                               UserCount = recipeGroup.Count()
                                           };

            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}