using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipeApplication.Data;
using RecipeApplication.Models;
using Microsoft.AspNetCore.Session;

namespace RecipeApplication.Controllers
{
    public class HomeController : Controller
    {
        //access database so you can check to see if user exists
        private RecipeBookContext db = new RecipeBookContext();
        
        public ActionResult Index(string username,string password)
        {
           
            if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var users = from user in db.Users select user;
                //handle incorrect password
                users = users.Where(u => u.Username.Equals(username));
                ViewBag.ErrorMessage = "No user found";
                ViewBag.ErrorType = "Username";
                if (users.Count() > 0)
                {
                    users = users.Where(u => u.Password.Equals(password));
                    if(users.Count() == 0)
                    {
                        ViewBag.ErrorMessage = "Incorrect password";
                        ViewBag.ErrorType = "Password";
                    }
                    else
                    {

                        //System.Diagnostics.Debug.WriteLine(user.Name);
                        //login successful
                        //set session data
                        Session["Username"] = username;
                        return RedirectToAction("Index", "Users");
                        //return RedirectToAction("Index", "Users",new { username = username});
                    }
                }
            }

            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}