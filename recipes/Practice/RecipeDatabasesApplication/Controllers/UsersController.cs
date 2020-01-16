//using System;
//using System.Collections.Generic;
using System.Data;
//using System.Data.Entity;
using System.Linq;
using System.Net;
//using System.Web;
using System.Web.Mvc;
using RecipeDatabasesApplication.DAL;
using RecipeDatabasesApplication.Models;
using RecipeDatabasesApplication.ViewModels;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace RecipeDatabasesApplication.Controllers
{
    public class UsersController : Controller
    {
        private RecipeDBContext db = new RecipeDBContext();

        // GET: Users
        public ActionResult Index(string order,string filter,string search,int? page)
        {
            ViewBag.CurrentSort = order;
            ViewBag.NameSortParam = string.IsNullOrEmpty(order) ? "desc" : "";
            if(search != null)
            {
                page = 1;
            }
            else
            {
                search = filter;
            }
            ViewBag.CurrentFilter = search;
            var users = from u in db.Users select u;
            if (!string.IsNullOrEmpty(search))
            {
                //search by name, this can be updated
                users = users.Where(u => u.Name.Contains(search));
            }
            switch (order)
            {
                case "desc": //name descending
                    users = users.OrderByDescending(u => u.Name);
                    break;
                default: //name ascending
                    users = users.OrderBy(u => u.Name);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber,pageSize));
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
        public ActionResult Create([Bind(Include = "UserID,Name,Username,Password")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                //Error message
                ModelState.AddModelError("", "Can't create new User");
            }
            

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var updateUser = db.Users.Find(id);
            if (TryUpdateModel(updateUser, "", new string[] { "Name", "Username", "Password" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to update user data");
                }
            }
            return View(updateUser);
            //return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id,bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Couldn't delete user";
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
