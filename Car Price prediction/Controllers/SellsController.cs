using Car_Price_prediction.Models;
using Car_Price_prediction.Services;
using Car_Price_prediction.view_model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sell_your_car.view_model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Price_prediction.Controllers
{
   
    public class SellsController : Controller
    {
        ApplicationDbcontext _context = new ApplicationDbcontext();
        [Authorize]
        public ActionResult SellYourCar ()
        {
            var userID = User.Identity.GetUserId();

            var currentDate = DateTime.Now.Date;
            var maxAllowedDate = currentDate.AddDays(-30);

            int car_count = _context.user_cars
                .Where(c => c.UserID == userID && DbFunctions.TruncateTime(c.InsertionDate) <= maxAllowedDate).Count();

            if (User.IsInRole("Dealer") && car_count >= 15 || !User.IsInRole("Dealer") && car_count >= 2)
            {
                throw new Exception("You have reached the limit of 20 car inserts for this month.");
                //return View("LimitedInsert");
            }

            var car_Brand = _context.Brands_Models.Select(c => c.Brand).Distinct().ToList();
            ViewBag.Brand = new MultiSelectList(car_Brand);
        
            var governorates = _context.Governorate.Select(c => new {
                GatID = c.id,
                GatName = c.governorate_name_en
            }).ToList();
            ViewBag.governorates = new MultiSelectList(governorates, "GatID", "GatName");
          

            List<FeatureSelection> featureSelection = _context.features.Select(x => new FeatureSelection
            {
                FeaturesID = x.ID,
                Features = x.Feature,
                ISselected = false,

            }).ToList();
            var ShowroomData = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == userID);
            if (User.IsInRole("Dealer"))
            {
                var model = new SellYourCarViewModel
                {
                    TraderName = ShowroomData.TraderName,
                    ShowroomName = ShowroomData.ShowroomName,
                    ShowroomDiscription = ShowroomData.Discription,
                    Logo = ShowroomData.Logo,
                    CoverImage = ShowroomData.CoverImage,
                    ShowroomPhoneNumber0 = ShowroomData.PhoneNumber0,
                    ShowroomPhoneNumber1 = ShowroomData.PhoneNumber1,
                    ShowroomEmail = ShowroomData.Email,

                    CarFeatures = featureSelection,
                    colors = new Colors().getcolors(),

                };
                return View(model);
            }
            else
            {
                var model = new SellYourCarViewModel
                {
                    CarFeatures = featureSelection,
                    colors = new Colors().getcolors(),
                    Name = _context.Users.Where(c => c.Id == userID).Select(c => c.Name).FirstOrDefault(),
                    Email = _context.Users.Where(c => c.Id == userID).Select(c => c.Email).FirstOrDefault(),
                    PhoneNumber = _context.Users.Where(c => c.Id == userID).Select(c => c.PhoneNumber).FirstOrDefault(),
                };
                return View(model);
            }
           
        }
        [HttpPost]
        [Authorize]
        public ActionResult SellYourCar(SellYourCarViewModel cars, HttpPostedFileBase image1,
            HttpPostedFileBase image2, HttpPostedFileBase image3,HttpPostedFileBase image4) {
            var car_Brand = _context.Brands_Models.Select(c => c.Brand).Distinct().ToList();
            ViewBag.Brand = new MultiSelectList(car_Brand);

      

            var governorates = _context.Governorate.Select(c => new {
                GatID = c.id,
                GatName = c.governorate_name_en
            }).ToList();
            ViewBag.governorates = new MultiSelectList(governorates, "GatID", "GatName");
            // ___________________________________________
            Cars car = new Cars();

            var userID = User.Identity.GetUserId();
            car.Brand = cars.Brand;
            car.Model = cars.Model;
            car.Body = cars.Body;
            car.Fuel = cars.Fuel;
            car.Color = cars.Color;
            car.Transmission = cars.Transmission;
            car.Year = cars.Year;
            car.Engine_CC = cars.Engine_CC;
            car.Kilometers_Driven = cars.Kilometers_Driven;
            car.Color = cars.Color;
            car.City = cars.City;
            car.Price = cars.Price;
            car.Fabrica = cars.Fabrica;
            car.Discription = cars.Discription;
            car.UserID = userID;
           
            //add the feature for car 


            CarImages carImages = new CarImages();
            /*   int lastcarId = _context.Cars.Max(item => item.ID);
               carImages.CarID = lastcarId;*/

            var selectedoptions_featureID = cars.CarFeatures.Where(x => x.ISselected == true).Select(x => x.FeaturesID).ToList();

            var addedList = selectedoptions_featureID.Select(x => new CarFeatures
            {
                FeatureID = x,
            }).ToList();
           

            if (image1 != null && image1.ContentLength > 0)
            {

                string ImageFileName1 = Path.GetFileName(image1.FileName);
                string FolderPath1 = Path.Combine(Server.MapPath("~/img/Car_Image"), ImageFileName1);
                image1.SaveAs(FolderPath1);


                carImages.Frontimage = ImageFileName1;
            }
            if (image2 != null && image2.ContentLength > 0)
            {

                string ImageFileName2 = Path.GetFileName(image2.FileName);
                string FolderPath2 = Path.Combine(Server.MapPath("~/img/Car_Image"), ImageFileName2);
                image2.SaveAs(FolderPath2);


                carImages.Backimage = ImageFileName2;

            }
            if (image3 != null && image3.ContentLength > 0)
            {
                string ImageFileName3 = Path.GetFileName(image3.FileName);
                string FolderPath3 = Path.Combine(Server.MapPath("~/img/Car_Image"), ImageFileName3);
                image3.SaveAs(FolderPath3);

                carImages.RightSideimage = ImageFileName3;
            }
            if (image4 != null && image4.ContentLength > 0)
            {
                string ImageFileName4 = Path.GetFileName(image4.FileName);
                string FolderPath4 = Path.Combine(Server.MapPath("~/img/Car_Image"), ImageFileName4);
                image4.SaveAs(FolderPath4);

                carImages.LeftSideimage = ImageFileName4;
            }
            _context.Cars.Add(car);
            _context.carFeatures.AddRange(addedList);
            _context.carImages.Add(carImages);
            _context.SaveChanges();


            return RedirectToAction("SellYourCar");
        }


        public ActionResult Car_For_Sell(int page = 1)
        {
            var car_model_year = _context.Cars.Select(c => c.Year).Distinct().ToList();
            ViewBag.year = new MultiSelectList(car_model_year);

            var car_model_price = _context.Cars.Select(c => c.Price).Distinct().ToList();
            ViewBag.Price = new MultiSelectList(car_model_price);

            var Engine_CC = _context.Cars.Select(c => c.Engine_CC).Distinct().ToList();
            ViewBag.car_Engine_CC = new MultiSelectList(Engine_CC);

            // prepare vm to be passsed to the view 
            // get car list from db
  
            List <car_detials_vm> Cars = _context.Cars.Select(x => new car_detials_vm
            {
                
                ID = x.ID,
                UserID = x.UserID,
                Brand = x.Brand,
                Model =x.Model,
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
            }).Where(x => x.UserID != null).Where(x => x.Active == true).ToList();

            // get make selection list
            List<MakerSelection> BrandList = _context.Cars.Select(x => new MakerSelection
            {
                Brand = x.Brand,
                ISselected = false
            }).Distinct().ToList();
            // get Size selection list
   
          
            var model = new CarListVM
            {
                Cars = Cars,
                Selections_FOR_MAKE = BrandList,
              
                Sizepage = 4,
                car_Data = Cars.OrderBy(d => d.ID),
                CurrentPage = page,
                Price0 = 0,
                Price1 = 3500000.0,

            };


            return View(model);
        }

        [HttpPost]
        public ActionResult Car_For_Sell(CarListVM model, int page = 1)
        {
            var car_model_year = _context.Cars.Select(c => c.Year).Distinct().ToList();
            ViewBag.year = new MultiSelectList(car_model_year);

            var car_model_price = _context.Cars.Select(c => c.Price).Distinct().ToList();
            ViewBag.Price = new MultiSelectList(car_model_price);

            var Engine_CC = _context.Cars.Select(c => c.Engine_CC).Distinct().ToList();
            ViewBag.car_Engine_CC = new MultiSelectList(Engine_CC);


            // get the selected make options say bmw and oudi
            var selectedoptions_for_Brand = model.Selections_FOR_MAKE.Where(x => x.ISselected == true).Select(x => x.Brand).ToList();
           // get car list from db
            model.Cars = _context.Cars.Select(x => new car_detials_vm
            {
                ID = x.ID,
                UserID =x.UserID,
                Brand = x.Brand,
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
            }).Where(c => selectedoptions_for_Brand.Contains(c.Brand) || selectedoptions_for_Brand.Count == 0)
                 .Where(x => x.yearRange >= model.year0 && x.yearRange <= model.year1 || x.yearRange >= model.year0 && model.year1 == null
                 || x.yearRange <= model.year1 && model.year0 == null || model.year0 == null && model.year1 == null)
                 .Where(x => x.PriceRange >= model.Price0 && x.PriceRange <= model.Price1 || model.Price0 == null || model.Price1 == null)
                 .Where(e => e.Fuel == model.Fuel || model.Fuel == null || model.Fuel == "")
                 .Where(e => e.Transmission == model.Transmission || model.Transmission == null || model.Transmission == "")
                 .Where(e => e.Engine_CC_Range >= model.Engine_CC0 && e.Engine_CC_Range <= model.Engine_CC1 || 
                  e.Engine_CC_Range >= model.Engine_CC0 && model.Engine_CC1 == null ||
                  e.Engine_CC_Range <= model.Engine_CC1 && model.Engine_CC0 == null || model.Engine_CC0 == null && model.Engine_CC1 == null)
                 .Where(n => n.City == model.City || model.City == null)
                 .Where(n => n.Body == model.Body || model.Body == null).Where(c=>c.UserID !=null).Where(x => x.Active == true).ToList();
            // get make selection list
            List<MakerSelection> BrandList = _context.Cars.Select(x => new MakerSelection
            {
                Brand = x.Brand,
                ISselected = false
            }).Distinct().ToList();

            var Carss = new CarListVM
            {
                Cars = model.Cars,
                Selections_FOR_MAKE = BrandList,
                Sizepage = 5,
                car_Data = model.Cars.OrderBy(d => d.ID),
                CurrentPage = page,
                Price0 = model.Price0,
                Price1 = model.Price1,
            };

            return View(Carss);
        }

        public ActionResult Detalis(int? id)
        {
            var foundID = _context.Cars.Find(id);
            if (foundID == null)
            {

                return RedirectToAction("notFound", "home");
            }
            try
            {
                    

                var userCar = _context.Cars.Select(x => new CarWithOner_vm
                {
                    ID = x.ID,
                    UserID = x.UserID,
                    Brand = x.Brand,
                    Body = x.Body,
                    Fuel = x.Fuel,
                    Engine_CC = x.Engine_CC,
                    Color = x.Color,
                    City = x.City,
                    Governorate = _context.Governorate.FirstOrDefault(c => c.id ==
                    _context.Cities.FirstOrDefault(v => v.city_name_en.Equals(x.City)).governorateID).governorate_name_en,
                    Transmission = x.Transmission,
                    Kilometers_Driven = x.Kilometers_Driven,
                    Fabrica = x.Fabrica,
                    Discription = x.Discription,
                    Frontimage = _context.carImages.FirstOrDefault(c => c.CarID == id).Frontimage,
                    Backimage = _context.carImages.FirstOrDefault(c => c.CarID == id).Backimage,
                    RightSideimage = _context.carImages.FirstOrDefault(c => c.CarID == id).RightSideimage,
                    LeftSideimage = _context.carImages.FirstOrDefault(c => c.CarID == id).LeftSideimage,
                    Year = x.Year,
                    Model = x.Model,
                    Price = x.Price,
                    PhoneNumber = x.User.PhoneNumber,
                    UserName = _context.Users.Where(c => c.UserName == x.User.UserName).Select(c => c.Name).FirstOrDefault(),
                    Email = x.User.Email,

                }).Where(c => c.ID == id).FirstOrDefault();
                return View(userCar);
            }
            catch (Exception)
            {
                return RedirectToAction("Error","Home");
               
            }

            
        }

        public JsonResult ModelSelctionbyAjax(string brand)
        {
            var models = _context.Brands_Models.Where(x => x.Brand == brand).Select(c => c.Model).Distinct().ToList();
            return Json(new SelectList(models, "Model"));
        }

        public JsonResult AreaSelctionbyAjax(int Gov)
        {
            var Citiys = _context.Cities.Where(c => c.governorateID == Gov).Select(x => x.city_name_en).ToList();
            Citiys.Sort();
            return Json(new SelectList(Citiys, "city_name_en"));
        }

        // test ajax to get list of cars 
        public JsonResult GetCarsListbyAjax(string brand)
        {
            var listofcars = _context.Cars.Where(c => c.Brand.Contains(brand)).ToList();
           
            return Json(new SelectList(listofcars, "cars"));
        }



    }


}

