using Car_Price_prediction.view_model;
using Car_Price_prediction.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Car_Price_prediction.Controllers
{
    [Authorize(Roles = "Admin")]
    public class adminController : Controller
    {
        // GET: admin
        ApplicationDbcontext _Context = new ApplicationDbcontext();
        public ActionResult USER_Accounts()
        {
            // map  Vm to identity user 
            var Users = _Context.Users.Select(x => new Registration_Vm
            {

                Name = x.Name,
                User_Name = x.UserName,
                Email = x.Email,
                phone_num = x.PhoneNumber,
                Age = x.age,
                ID = x.Id,
                Password = x.PasswordHash

            }).ToList();

            return View(Users);
        }
        public ActionResult User_massage()
        {
            var messages = _Context.CustomerMessages.ToList();
            return View(messages);
        }
        public ActionResult SellsCar()
        {
            var sells = _Context.Cars.Select(x => new SellsVM
            {
                ID = x.ID,
                UserID = x.UserID,
                Brand = x.Brand,
                Body = x.Body,
                Fuel = x.Fuel,
                Engine_CC = x.Engine_CC,
                Color = x.Color,
                City = x.City,
                Transmission = x.Transmission,
                Kilometers_Driven = x.Kilometers_Driven,
                Fabrica = x.Fabrica,
                Discription = x.Discription,
                Frontimage = _Context.carImages.FirstOrDefault(c =>c.CarID ==x.ID).Frontimage,
                Backimage = _Context.carImages.FirstOrDefault(c => c.CarID == x.ID).Backimage,
                RightSideimage = _Context.carImages.FirstOrDefault(c => c.CarID == x.ID).RightSideimage,
                Year = x.Year,
                Model = x.Model,
                Price = x.Price,
                Email = x.User.Email,
                PhoneNumber = x.User.PhoneNumber,
                UserName = _Context.Users.FirstOrDefault(a=>a.Id == x.UserID).Name
            }).Where(c => c.UserID != null)
            .ToList();
            return View(sells);
        }

        public ActionResult BussnesAccount()
        {


            var rols = _Context.Roles.Select(x => x.Name).ToList();
            ViewBag.Roles = new MultiSelectList(rols);

            return View();
        }

        [HttpPost]
        public ActionResult BussnesAccount(Registration_Vm Reg, String RoleName, HttpPostedFileBase image)
        {
            var rols = _Context.Roles.Select(x => x.Name).ToList();
            ViewBag.Roles = new MultiSelectList(rols);
            if (ModelState.IsValid)
            {
                ApplicationDbcontext _Context = new ApplicationDbcontext();
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(_Context);
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);
                // map  Vm to identity user 
                ApplicationUser identityUser = new ApplicationUser();
                identityUser.UserName = Reg.User_Name;
                identityUser.Name = Reg.Name;
                identityUser.Email = Reg.Email;
                identityUser.PhoneNumber = Reg.phone_num;
                identityUser.Buss_img = Path.GetFileName(image.FileName);
                ViewBag.img = identityUser.Buss_img.Where(c => c.Equals(User.Identity.Name == identityUser.Name)).ToList();

                IdentityResult result = manager.Create(identityUser, Reg.Password);
                if (result.Succeeded && image.ContentLength > 0)
                {
                    manager.AddToRole(identityUser.Id, RoleName);

                    string ImageFileName = Path.GetFileName(image.FileName);

                    string FolderPath = Path.Combine(Server.MapPath("~/img/User_img/") + ImageFileName);
                    image.SaveAs(FolderPath);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item);
                    }

                }
            }
            return View(Reg);
        }
    
    
       [HttpPost]
        public ActionResult Delete(string UserId)
        {
            var foundID = _Context.Users.Find(UserId);
            if (foundID == null)
            {

                return RedirectToAction("notFound", "home");
            }
            try
            {
                var user = _Context.Users.Find(UserId);
                _Context.Users.Remove(user);
                _Context.SaveChanges();
                return RedirectToAction("USER_Accounts");
            }
            catch (Exception)
            {
                return RedirectToAction("error");
                throw;
            }
        }


        public ActionResult Deletecar(int Id)
        {
            var foundID = _Context.Cars.Find(Id);
            if (foundID == null)
            {

                return RedirectToAction("notFound", "home");
            }
            try
            {
                var car = _Context.Cars.Find(Id);
                _Context.Cars.Remove(car);
                _Context.SaveChanges();
                return RedirectToAction("SellsCar");
            }
            catch (Exception)
            {
                return RedirectToAction("error");
                throw;
            }

        }
    }

}