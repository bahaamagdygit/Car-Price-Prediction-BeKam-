using Car_Price_prediction.view_model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Price_prediction.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ApplicationDbcontext _context;
        public AccountController()
        {
            _context = new ApplicationDbcontext();
        }
        // GET: Account
        public ActionResult Login()
        {
           /* return PartialView("_LoginPartial");*/
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login_Vm log ,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbcontext _Context = new ApplicationDbcontext();
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(_Context);
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);
                ApplicationUser identityUser = manager.FindByName(log.User_Name);
                bool found = manager.CheckPassword(identityUser, log.Password);
                if (found)
                {
                    // Create cookies
                    SignInManager<ApplicationUser, String> signInManager = new SignInManager<ApplicationUser, String>
                         (manager, HttpContext.GetOwinContext().Authentication);
                          signInManager.SignIn(identityUser, true, false);
                    
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }else
                         return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User name or Password is  not Correct");
                }

            }

          
            return View(log);
            
        }

    

        public ActionResult Registration()
        {
            return View();

        }
        [HttpPost]

        public ActionResult Registration(Registration_Vm Reg, string returnUrl)
        {
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
                
                IdentityResult result = manager.Create(identityUser, Reg.Password);
                if (result.Succeeded)
                {
                   // manager.AddToRole(identityUser.Id, "admin");
                    // Create cookies
                    SignInManager<ApplicationUser, String> signInManager = new SignInManager<ApplicationUser, String>
                         (manager, HttpContext.GetOwinContext().Authentication);
                    signInManager.SignIn(identityUser, true, false);

                    

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
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
        [Authorize]
        public ActionResult Logout()
        {
            // delete cookies 
            HttpContext.GetOwinContext().Authentication.SignOut(
                DefaultAuthenticationTypes.ApplicationCookie
                );
            return RedirectToAction("index", "Home");
        }
        [Authorize]
        [HttpGet]
        public ActionResult UserProfile(int page = 1)
        {
          if(User.IsInRole("Dealer") || User.IsInRole("Admin")){
                return RedirectToAction("login", "account");
            }
                var UserID = User.Identity.GetUserId();

                List<car_detials_vm> Cars = _context.Cars.Select(x => new car_detials_vm
                {

                    ID = x.ID,
                    UserID = x.UserID,
                    Brand = x.Brand,
                    Model = x.Model,
                    Body = x.Body,
                    Fuel = x.Fuel,
                    Engine_CC_Range = x.Engine_CC,
                    Color = x.Color,
                    City = x.City,
                    Governorate = _context.Governorate.FirstOrDefault(c => c.id ==
                    _context.Cities.FirstOrDefault(v => v.city_name_en.Equals(x.City)).governorateID).governorate_name_en,
                    Transmission = x.Transmission,
                    Kilometers_Driven = x.Kilometers_Driven,
                    Fabrica = x.Fabrica,
                    yearRange = x.Year,
                    PriceRange = x.Price,
                    Active = x.Active,
                    Frontimage = _context.carImages.FirstOrDefault(v => v.CarID == x.ID) != null ?
                  _context.carImages.FirstOrDefault(v => v.CarID == x.ID).Frontimage.ToString() : null,
                    Backimage = _context.carImages.FirstOrDefault(v => v.CarID == x.ID) != null ?
                  _context.carImages.FirstOrDefault(v => v.CarID == x.ID).Backimage.ToString() : null,
                    RightSideimage = _context.carImages.FirstOrDefault(v => v.CarID == x.ID) != null ?
                  _context.carImages.FirstOrDefault(v => v.CarID == x.ID).RightSideimage.ToString() : null,
                    LeftSideimage = _context.carImages.FirstOrDefault(v => v.CarID == x.ID) != null ?
                  _context.carImages.FirstOrDefault(v => v.CarID == x.ID).LeftSideimage.ToString() : null,
                    UserName = x.User.UserName,
                    Phone_Namber = x.User.PhoneNumber
                }).Where(x => x.UserID.Equals(UserID)).ToList();

            var defultImg = "Logo1.png";
  

            UserProfile data = new UserProfile
            {
                Cars = Cars,
                

                UserName = _context.Users.FirstOrDefault(x => x.Id == UserID).Name != null ?
                    _context.Users.FirstOrDefault(x => x.Id == UserID).Name.ToString() : null,

                Email = _context.Users.FirstOrDefault(x => x.Id == UserID).Email != null ?
                    _context.Users.FirstOrDefault(x => x.Id == UserID).Email.ToString() : null,

                PhoneNumber = _context.Users.FirstOrDefault(x => x.Id == UserID).PhoneNumber != null ?
                    _context.Users.FirstOrDefault(x => x.Id == UserID).PhoneNumber.ToString() : null,

                UserImage = _context.Users.Find(UserID).Buss_img != null ?
                   _context.Users.Find(UserID).Buss_img : defultImg,


                    Sizepage = 4,
                    car_Data = Cars.OrderBy(d => d.ID),
                    CurrentPage = page,

                };


                return View(data);
         
        }
        [HttpPost]
        public ActionResult UserProfile(UserProfile Userinfo, HttpPostedFileBase userimage, int page = 1)
        {

            if (ModelState.IsValid)
            {
                var UserID = User.Identity.GetUserId();
                var identityUser = _context.Users.Find(UserID);
               
                if(Userinfo!= null && identityUser!= null)
                {
                   
                    identityUser.Name = Userinfo.UserName;
                    identityUser.Email = Userinfo.Email;
                    identityUser.PhoneNumber = Userinfo.PhoneNumber;
                    _context.SaveChanges();
                }
               
                if (userimage != null && userimage.ContentLength > 0)
                {

                    string ImageFileName1 = Path.GetFileName(userimage.FileName);
                    string FolderPath1 = Path.Combine(Server.MapPath("~/img/User"), ImageFileName1);
                    userimage.SaveAs(FolderPath1);


                    identityUser.Buss_img = ImageFileName1;
                }
            
              

                List<car_detials_vm> Cars = _context.Cars.Select(x => new car_detials_vm
                {

                    ID = x.ID,
                    UserID = x.UserID,
                    Brand = x.Brand,
                    Model = x.Model,
                    Body = x.Body,
                    Fuel = x.Fuel,
                    Engine_CC_Range = x.Engine_CC,
                    Color = x.Color,
                    City = x.City,
                    Governorate = _context.Governorate.FirstOrDefault(c => c.id ==
                    _context.Cities.FirstOrDefault(v => v.city_name_en.Equals(x.City)).governorateID).governorate_name_en,
                    Transmission = x.Transmission,
                    Kilometers_Driven = x.Kilometers_Driven,
                    Fabrica = x.Fabrica,
                    yearRange = x.Year,
                    PriceRange = x.Price,
                    Active = x.Active,
                    Frontimage = _context.carImages.FirstOrDefault(v => v.CarID == x.ID) != null ?
                  _context.carImages.FirstOrDefault(v => v.CarID == x.ID).Frontimage.ToString() : null,
                    Backimage = _context.carImages.FirstOrDefault(v => v.CarID == x.ID) != null ?
                  _context.carImages.FirstOrDefault(v => v.CarID == x.ID).Backimage.ToString() : null,
                    RightSideimage = _context.carImages.FirstOrDefault(v => v.CarID == x.ID) != null ?
                  _context.carImages.FirstOrDefault(v => v.CarID == x.ID).RightSideimage.ToString() : null,
                    LeftSideimage = _context.carImages.FirstOrDefault(v => v.CarID == x.ID) != null ?
                  _context.carImages.FirstOrDefault(v => v.CarID == x.ID).LeftSideimage.ToString() : null,
                    UserName = x.User.UserName,
                    Phone_Namber = x.User.PhoneNumber
                }).Where(x => x.UserID.Equals(UserID)).ToList();


                var defultImg = "Logo1.png";
                UserProfile data = new UserProfile
                {
                    Cars = Cars,

                    UserName = _context.Users.FirstOrDefault(x => x.Id == UserID).Name != null ?
                    _context.Users.FirstOrDefault(x => x.Id == UserID).Name.ToString() : null,

                    Email = _context.Users.FirstOrDefault(x => x.Id == UserID).Email != null ?
                    _context.Users.FirstOrDefault(x => x.Id == UserID).Email.ToString() : null,

                    PhoneNumber = _context.Users.FirstOrDefault(x => x.Id == UserID).PhoneNumber != null ?
                    _context.Users.FirstOrDefault(x => x.Id == UserID).PhoneNumber.ToString() : null,

                    UserImage = _context.Users.Find(UserID).Buss_img != null ?
                   _context.Users.Find(UserID).Buss_img : defultImg,


                    Sizepage = 4,
                    car_Data = Cars.OrderBy(d => d.ID),
                    CurrentPage = page,

                };


                return View(data);
            }
            else
                return RedirectToAction("error");
        }
    }
}