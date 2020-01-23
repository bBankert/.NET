using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpdeskTickets.Data;
using HelpdeskTickets.Models;

namespace HelpdeskTickets.Controllers
{
    public class UsersController : Controller
    {
        private SystemContext db = new SystemContext();

        //check for unique username
        public JsonResult UserExists(string username)
        {
            return Json(!db.Users.Any(u => u.Username == username), JsonRequestBehavior.AllowGet);
        }

        // GET: Users
        public ActionResult Index()
        {
            try
            {
                User user = this.Session["user"] as User;
                //System.Diagnostics.Debug.WriteLine(user.Name);
                //if user, only show tickets they created
                if (user.Permission.Equals(1))
                {
                    var tickets = from ticket in db.Tickets
                                  where user.UserId == ticket.Creator.UserId
                                  select ticket;
                    return View(tickets.ToList());
                }
                //if staff (IT), show tickets they created or have been assigned to them
                else
                {

                    var tickets = from ticket in db.Tickets
                                  where user.UserId == ticket.Creator.UserId || user.UserId == ticket.Owner.UserId
                                  select ticket;
                    return View(tickets.ToList());
                }
                
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            //return View();
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
        public ActionResult Create([Bind(Include = "UserId,Name,Username,Password,Permission")] User user)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Name,Username,Password,Permission")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
