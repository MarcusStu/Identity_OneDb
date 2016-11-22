using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using MarcusOneDbTest.Models;
using MarcusOneDbTest.Controllers;

namespace Identity_Assignment.Controllers
{
    [OverrideAuthorization]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        ApplicationDbContext context;

        public RoleController()
        {
            context = new ApplicationDbContext();
        }

        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);

        }

        //public Boolean isAdminUser()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var user = User.Identity;
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
        /// <summary>
        /// Create  a New role
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        /// <summary>
        /// Create a New Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        /// 

        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            var allRoles = context.Roles.ToList();
            //ViewBag.AllRoles = allRoles;
            ViewBag.AllRoles = new SelectList(context.Roles, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult Delete(string AllRoles)
        {
            var rStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(rStore);

            IdentityRole Role = roleManager.FindById(AllRoles);

            roleManager.Delete(Role);

            ViewBag.RoleStatus = AllRoles + " successfully deleted!";

            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult DeleteRoleFromUser()
        {
            // Get list of Roles
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            // Get list of Users
            var userList = context.Users.OrderBy(r => r.UserName).ToList().Select(rr => new SelectListItem { Value = rr.UserName.ToString(), Text = rr.UserName }).ToList();
            ViewBag.Users = userList;

            return View();
        }

        [HttpPost]
        public ActionResult DeleteRoleFromUser(string Users, string Roles)
        {
            // Find user
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(Users, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var uStore = new UserStore<ApplicationUser>(context);
            var account = new UserManager<ApplicationUser>(uStore);
            var svar = account.RemoveFromRole(user.Id, Roles);

            ViewBag.ResultMessage = "User: " + Users + " was successfully removed from role " + Roles + "!";

            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}