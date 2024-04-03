using Car_Price_prediction.Models;
using Car_Price_prediction.Services;
using Car_Price_prediction.view_model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Price_prediction.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbcontext _context = new ApplicationDbcontext();

       
        public ActionResult Index()
        {
            AccountConstraints accountConstraints = new AccountConstraints();
            accountConstraints.calendar();

            var car_model_year = _context.Cars.Select(c => c.Year).Distinct().ToList();
            ViewBag.year = new MultiSelectList(car_model_year);
            var car_model_price = _context.Cars.Select(c => c.Price).Distinct().ToList();
            ViewBag.Price = new MultiSelectList(car_model_price);
            var car_engine_HP = _context.Cars.Select(c => c.Engine_CC).Distinct().ToList();
            ViewBag.car_engine_HP = new MultiSelectList(car_engine_HP);

            // prepare vm to be passsed to the view 
            // get car list from db


            List<car_detials_vm> cars = _context.Cars.Select(x => new car_detials_vm
            {
                ID = x.ID,
                Brand = x.Brand,
                Fuel = x.Fuel,
                Engine_CC_Range = x.Engine_CC,
                Body = x.Body,
                Transmission = x.Transmission,
                yearRange = x.Year,
                PriceRange = x.Price,
                UserID = x.UserID,
                image = _context.carImages.FirstOrDefault(v => v.CarID == x.ID) != null ?
                _context.carImages.FirstOrDefault(v => v.CarID == x.ID).Frontimage.ToString() : null,

            }).Where(a => a.UserID != null).ToList();

      /*      var DealerListID = _context.showroomsForTrader.Select(c=>c.UserID).ToList();*/

            // get make selection list
           var model = new CarListVM
            {
               Dealars = _context.showroomsForTrader.Select(x => new DealerVM
               {
                   ID = x.ID,
                   Logo = x.Logo,
                   TraderName = x.TraderName,
                   UserID = x.UserID
               }).Distinct().ToList(),
               Cars = cars,     
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult notFound()
        {
            
            return View();
        }
        public ActionResult ServerError()
        {

            return View();
        }
        public ActionResult Contact()
        {

          /*  ViewBag.Message = "Your contact page.";*/

            return View();
        }
        [HttpPost]
        public ActionResult Contact(CustomerMessage mesge)
        {

           /* ViewBag.Message = "Your contact page.";*/

            try
            {
                if (ModelState.IsValid)
                {

                    var userID = User.Identity.GetUserId();
                    mesge.UserID = userID;
                    _context.CustomerMessages.Add(mesge);
                    _context.SaveChanges();

                    ViewBag.Message = "Thanks For Your Message";
                    ModelState.Clear();
                    return View();
                }else
                    return View(mesge);
            }
            catch (Exception )
            {

                ModelState.AddModelError("", "There is Invalid Input");
                return View(mesge);
            }
            
        }
        public ActionResult Slider()
        {
            var ID = User.Identity.GetUserId();
            List<DealerVM> Dealers = _context.showroomsForTrader.Select(x => new DealerVM
            {
                ID = x.ID,
                Logo = x.Logo,
                TraderName = x.TraderName,
                UserID = x.UserID
                
            }).ToList();

            var model = new CarListVM
            {

                Dealars = Dealers,

            };

            return PartialView(model);
        }


        public ActionResult Dealers()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public ActionResult Showroom(int page = 1)
        {
            List<ShowroomsForTrader> Showrooms = _context.showroomsForTrader.ToList();
            var model = new ShowroomsWM
            {
                Showrooms = Showrooms,
                Sizepage = 8,
                Showroom_Data = Showrooms.OrderBy(d => d.ID),
                CurrentPage = page,
            };
            return View(model);
        }

        public ActionResult ShowroomDetalis(int id, int page = 1)
        {
            var DealerID = _context.showroomsForTrader.FirstOrDefault(x => x.ID == id).UserID;

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
            }).Where(x => x.UserID == DealerID).Where(x => x.Active == true).ToList();

            List<locations_vm> Branches = _context.locations.Select(x => new locations_vm
            {
                ShoowroomId = x.ShoowroomId,
                Governorate = x.Governorate,
                City = x.City,
                Street = x.Street,
                Neighborhood = x.Neighborhood,
            }).Where(x => x.ShoowroomId == id).ToList();


            var model = new DealerVM
            {
                Cars = Cars,
                Branches = Branches,
                ShowroomID = id,
                TraderName = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).TraderName,
                ShowroomName = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).ShowroomName,
                PhoneNumber0 = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).PhoneNumber0,
                PhoneNumber1 = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).PhoneNumber1,
                Email = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).Email,
                Discription = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).Discription,
                Logo = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).Logo,
                CoverImage = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).CoverImage,

                Sizepage = 8,
                car_Data = Cars.OrderBy(d => d.ID),
                CurrentPage = page,


            };


            return View(model);
        }

        public ActionResult ShowroomReviews(int id)
        {
            var reviews = _context.ShowroomReviews.Where(s => s.ShowroomID == id).ToList();
            var newReview = new ShowroomReviews();
            return View(Tuple.Create(newReview, reviews, id));
            //return View(reviews);
        }


        [HttpPost]
        [Authorize]
        public ActionResult ShowroomReviews(ShowroomReviews review)
        {
           
            var newReview = new ShowroomReviews();
            if (ModelState.IsValid)
            {
                var UseData = _context.Users.FirstOrDefault(x => x.Id == review.UserID);
                review.Name = UseData.Name;
                review.Email = UseData.Email;
                review.phoneNumber = UseData.PhoneNumber;
                review.data = DateTime.Now;
                _context.ShowroomReviews.Add(review);
                _context.SaveChanges();
                var allreviews = _context.ShowroomReviews.Where(s => s.ShowroomID == review.ShowroomID).ToList();
                
                return View(Tuple.Create(newReview, allreviews, review.ShowroomID));
            }

            return View("ShowroomReviews", review);
        }
    }
}