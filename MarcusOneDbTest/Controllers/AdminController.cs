using MarcusOneDbTest.Controllers;
using MarcusOneDbTest.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Identity_Assignment.Controllers
{
    [OverrideAuthorization]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext context;

        public AdminController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Users
        public ActionResult Index()
        {
            var user = User.Identity;
            ViewBag.Name = user.Name;

            var allUsers = (new ApplicationDbContext()).Users.OrderBy(r => r.UserName).ToList();
            ViewBag.AllUsers = allUsers;

            var allRoles = context.Roles.ToList();
            ViewBag.AllRoles = allRoles;

            var userList = context.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
            ViewBag.Users = userList;

            return View();
        }

        //public Boolean isAdminUser()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var user = User.Identity;
        //        ApplicationDbContext context = new ApplicationDbContext();
        //        var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        //        var s = UserManager.GetRoles(user.GetUserId());
        //        if (s[0].ToString() == "Admin")
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return false;
        //}


        public ActionResult Assign()
        {
            // Get users in a dropdown list
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            // Get users in a dropdown list
            var userList = context.Users.OrderBy(d => d.UserName).ToList().Select(dd => new SelectListItem { Value = dd.UserName.ToString(), Text = dd.UserName }).ToList();
            ViewBag.Users = userList;

            return View();
        }

        [HttpPost]
        public ActionResult Assign(string Users, string Roles)
        {
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(Users, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var uStore = new UserStore<ApplicationUser>(context);
            var account = new UserManager<ApplicationUser>(uStore);
            account.AddToRole(user.Id, Roles);

            // Required to pass this in the POST as well or it will give an error in the View
            // Get users in a dropdown list
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            // Required to pass this in the POST as well or it will give an error in the View
            // Get users in a dropdown list
            var userList = context.Users.OrderBy(d => d.UserName).ToList().Select(dd => new SelectListItem { Value = dd.UserName.ToString(), Text = dd.UserName }).ToList();
            ViewBag.Users = userList;

            ViewBag.ResultMessage = "User: " + Users + " was successfully added to role " + Roles + "!";

            return View("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string Users)
        {
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(Users, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var uStore = new UserStore<ApplicationUser>(context);
            var account = new UserManager<ApplicationUser>(uStore);

            ViewBag.RolesForThisUser = account.GetRoles(user.Id);

            // Required to pass this in the POST as well or it will give an error in the View
            // List of roles
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            // Required to pass this in the POST as well or it will give an error in the View
            // List of users
            var userList = context.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
            ViewBag.Users = userList;

            return View("Index");
        }
    }
}