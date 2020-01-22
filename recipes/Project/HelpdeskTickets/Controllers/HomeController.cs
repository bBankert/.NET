using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpdeskTickets.Data;
using HelpdeskTickets.Models;

namespace HelpdeskTickets.Controllers
{
    public class HomeController : Controller
    {
        private SystemContext db = new SystemContext();
        public ActionResult Index(string username,string password)
        {
            if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    User user = db.Users.Single(u => u.Username.Equals(username) && u.Password.Equals(password));
                        this.Session["user"] = user;
                        //System.Diagnostics.Debug.WriteLine(user.Name);
                        return RedirectToAction("Index", "Users");
                }
                catch (Exception)
                {
                    ViewBag.Error = "Couldn't log in, please try again";
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