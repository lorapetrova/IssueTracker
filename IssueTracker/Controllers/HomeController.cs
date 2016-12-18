using IssueTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            var context = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(u => u.Id == userId);
            context.Issues.Add(new Entities.Issue
            {
                Author = user,
                AuthorId = user.Id,
                Title = "My First Issue",
                Description = "Foo Bar Baz",
                State = Entities.IssueState.New,
                SubmitionDate = DateTime.Today
            });
            context.SaveChanges();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}