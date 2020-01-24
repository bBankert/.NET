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
    public class TicketsController : Controller
    {
        private SystemContext db = new SystemContext();

        // GET: Tickets
        public ActionResult Index()
        {
            return View(db.Tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketId,Title,Description,Progress,Creator")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                User user = this.Session["user"] as User;
                ticket.Creator = db.Users.Single(u => u.UserId == user.UserId);
                
                db.Tickets.Add(ticket);
                db.SaveChanges();
                
                return RedirectToAction("Index","Users");
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Ticket ticket = db.Tickets.Find(id);
                User user = this.Session["user"] as User;
                ViewBag.permission = user.Permission;
                //get the users who are staff members
                var staffMembers = from usr in db.Users
                                   where usr.Permission == Permission.Staff
                                   select usr;
                //make a select list
                List<SelectListItem> items = new List<SelectListItem>();
                foreach(var staffMember in staffMembers)
                {
                    //default
                    if(items.Count() == 0)
                    {
                        SelectListItem item = new SelectListItem() { Text = staffMember.Name, Value = (staffMember.UserId).ToString(),Selected = true };
                        items.Add(item);
                    }
                    else
                    {
                        SelectListItem item = new SelectListItem() { Text = staffMember.Name, Value = (staffMember.UserId).ToString() };
                        items.Add(item);
                    }
                    
                }
                ViewBag.Owner = items;
                if (ticket == null)
                {
                    return HttpNotFound();
                }
                return View(ticket);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            
            
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketId,Title,Description,Progress")] Ticket ticket)
        {
            //System.Diagnostics.Debug.WriteLine(Request.Form);
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Request.Form["owner"]))
                {
                    int OwnerId = Int32.Parse(Request.Form["Owner"]);
                    User user = db.Users.Single(u => u.UserId == OwnerId);
                    var ticketToUpdate = db.Tickets.Find(ticket.TicketId);
                    ticketToUpdate.Owner = user;
                    
                }
                db.SaveChanges();
                
                return RedirectToAction("Index","Users");
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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
