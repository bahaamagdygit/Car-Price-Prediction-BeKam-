
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Price_prediction.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult addRole ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addRole(String RoleName)
        {
            ApplicationDbcontext _Context = new ApplicationDbcontext();
            RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(_Context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(store);

            IdentityRole role = new IdentityRole();

            role.Name = RoleName;
            roleManager.Create(role);
            return RedirectToAction("Index", "home");
        }
    }
}