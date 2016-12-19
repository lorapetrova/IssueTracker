using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IssueTracker.Entities;
using IssueTracker.Models;
using Microsoft.AspNet.Identity;

namespace IssueTracker.Controllers
{
    [Authorize]
    public class IssuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        // GET: Issues
        public ActionResult Index()
        {
            var issues = db.Issues.Include(i => i.Author);
            return View(issues.ToList());
        }
        
        [AllowAnonymous]
        // GET: Issues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            var author = issue.Author;
            return View(issue);
        }

        // GET: Issues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Issues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,State")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(u => u.Id == userId);

                issue.Author = user;
                issue.AuthorId = userId;
                issue.SubmitionDate = DateTime.Today;
                db.Issues.Add(issue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(issue);
        }

        // GET: Issues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,State")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                var toUpdate = db.Issues.Find(issue.Id);
                toUpdate.Title = issue.Title;
                toUpdate.Description = issue.Description;
                toUpdate.State = issue.State;
                db.Entry(toUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        // GET: Issues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Issue issue = db.Issues.Find(id);
            db.Issues.Remove(issue);
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
