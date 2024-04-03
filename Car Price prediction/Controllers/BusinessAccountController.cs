using Car_Price_prediction.Models;
using Car_Price_prediction.view_model;
using Microsoft.AspNet.Identity;
using Sell_your_car.view_model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Price_prediction.Controllers
{
    [Authorize(Roles = "Dealer")]

    public class BusinessAccountController : Controller
    {

        private readonly ApplicationDbcontext _context;

        public BusinessAccountController()
        {
            _context = new ApplicationDbcontext();
        }

        [HttpGet]
        public ActionResult DealerProfile()
        {
            var userID = User.Identity.GetUserId();
            var getfirest = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == userID);
            var showroomID = getfirest != null ? getfirest.ID : 0;

            var getlocation = _context.locations.FirstOrDefault(x => x.ShoowroomId == showroomID);
            if (getfirest == null)
            {
                // object has defult value 
                var showroom1 = new ShowroomsWithLocation
                {
                    Acc_name = _context.Users.FirstOrDefault(x => x.Id == userID).Name,
                    Userimage = _context.Users.FirstOrDefault(x => x.Id == userID).Buss_img,
                    TraderName = "Trader Name",
                    ShowroomName = "Showroom Name",
                    PhoneNumber0 = "Phone Nummber (1)",
                    PhoneNumber1 = "Phone Nummber (2)",
                    Email = "your Email Address",
                    Logo = "logo.png",
                    CoverImage = "profile-cover.jpg",
                    Discription = " wrire the Discription about your showroom",
                    City = "City",
                    Governorate = "Governorate",
                    Neighborhood = "Neighborhood",
                    Street = "Street",


                };
                return View(showroom1);
            }
            else
            {
                // get list of location for this showroom

                /*    List<Location> locations = _context.locations.Where(c => c.ShoowroomId == showroomID).Select(x => new Location
                    {
                        ID = x.ID,
                        Governorate = x.Governorate,
                        City = x.City,
                        Street = x.Street,
                        Neighborhood = x.Neighborhood,
                    }).ToList();*/

                // get showroom for the user how authonticate
                var showroom = new ShowroomsWithLocation
                {
                    ID = getfirest != null ? getfirest.ID : 0,
                    Userimage = _context.Users.FirstOrDefault(x => x.Id == userID).Buss_img,
                    Acc_name = _context.Users.FirstOrDefault(x => x.Id == userID).Name,
                    TraderName = getfirest.TraderName != null ? getfirest.TraderName.ToString() : null,
                    ShowroomName = getfirest.ShowroomName != null ? getfirest.ShowroomName.ToString() : null,
                    PhoneNumber0 = getfirest.PhoneNumber0 != null ? getfirest.PhoneNumber0.ToString() : null,
                    PhoneNumber1 = getfirest.PhoneNumber1 != null ? getfirest.PhoneNumber1.ToString() : null,
                    Email = getfirest.Email != null ? getfirest.Email.ToString() : null,
                    Discription = getfirest.Discription != null ? getfirest.Discription.ToString() : null,
                    Logo = getfirest.Logo != null ? getfirest.Logo.ToString() : "logo.png",
                    CoverImage = getfirest.CoverImage != null ? getfirest.CoverImage.ToString() : "profile-cover.jpg",

                    City = getlocation != null ? getlocation.City.ToString() : null,
                    Governorate = getlocation != null ? getlocation.Governorate.ToString() : null,
                    Street = getlocation != null ? getlocation.Street.ToString() : null,
                    Neighborhood = getlocation != null ? getlocation.Neighborhood.ToString() : null,

                };
                return View(showroom);
            }

        }
        [HttpPost]
        public ActionResult DealerProfile(ShowroomsWithLocation model, HttpPostedFileBase Logo, HttpPostedFileBase CoverImage)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var userID = User.Identity.GetUserId();
                    var getshowroom = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == userID);
                    var showroomID = getshowroom != null ? getshowroom.ID : 0;
                    var getlocation = _context.locations.FirstOrDefault(x => x.ShoowroomId == showroomID);

                    // if dosen't exist >>  create new one  (insert)
                    if (getshowroom == null)
                    {
                        // get Object from showroom
                        ShowroomsForTrader showrom = new ShowroomsForTrader();
                        showrom.TraderName = model.TraderName;
                        showrom.ShowroomName = model.ShowroomName;
                        showrom.PhoneNumber0 = model.PhoneNumber0;
                        showrom.PhoneNumber1 = model.PhoneNumber1;
                        showrom.Email = model.Email;
                        showrom.Discription = model.Discription;
                        showrom.Logo = Logo.FileName;
                        showrom.CoverImage = CoverImage.FileName;
                        showrom.UserID = userID;

                        Location location = new Location();
                        location.ShoowroomId = showroomID;
                        location.City = model.City;
                        location.Neighborhood = model.Neighborhood;
                        location.Street = model.Street;
                        location.Governorate = model.Governorate;
                        location.City = model.City;

                        // set logo and background 
                        if (Logo != null && Logo.ContentLength > 0)
                        {

                            string ImageFileName1 = Path.GetFileName(Logo.FileName).Replace(" ", String.Empty);
                            string FolderPath1 = Path.Combine(Server.MapPath("~/img/Delear_logo"), ImageFileName1);
                            Logo.SaveAs(FolderPath1);
                            showrom.Logo = ImageFileName1;
                            model.Logo = ImageFileName1;
                        }
                        if (CoverImage != null && CoverImage.ContentLength > 0)
                        {
                            string ImageFileName2 = Path.GetFileName(CoverImage.FileName).Replace(" ", String.Empty);
                            string FolderPath2 = Path.Combine(Server.MapPath("~/img/Delear_background"), ImageFileName2);
                            CoverImage.SaveAs(FolderPath2);
                            showrom.CoverImage = ImageFileName2;
                            model.CoverImage = ImageFileName2;
                        }
                        model.Acc_name = _context.Users.FirstOrDefault(x => x.Id == userID).Name;
                        model.Userimage = _context.Users.FirstOrDefault(x => x.Id == userID).Buss_img;
                        _context.showroomsForTrader.Add(showrom);
                        _context.locations.Add(location);
                        _context.SaveChanges();

                        return View(model);
                    }
                    if (getshowroom != null)
                    {
                        getshowroom.TraderName = model.TraderName;
                        getshowroom.ShowroomName = model.ShowroomName;
                        getshowroom.PhoneNumber0 = model.PhoneNumber0;
                        getshowroom.PhoneNumber1 = model.PhoneNumber1;
                        getshowroom.Email = model.Email;
                        getshowroom.Discription = model.Discription;

                        if (Logo != null && Logo.ContentLength > 0)
                        {

                            string ImageFileName1 = Path.GetFileName(Logo.FileName).Replace(" ", String.Empty);
                            string FolderPath1 = Path.Combine(Server.MapPath("~/img/Delear_logo"), ImageFileName1);
                            Logo.SaveAs(FolderPath1);
                            getshowroom.Logo = ImageFileName1;
                            model.Logo = ImageFileName1;

                        }
                        if (CoverImage != null && CoverImage.ContentLength > 0)
                        {
                            string ImageFileName2 = Path.GetFileName(CoverImage.FileName).Replace(" ", String.Empty) ;
                           
                            string FolderPath2 = Path.Combine(Server.MapPath("~/img/Delear_background"), ImageFileName2);
                            CoverImage.SaveAs(FolderPath2);
                            getshowroom.CoverImage = ImageFileName2;
                            model.CoverImage = ImageFileName2;
                        }


                        getlocation.City = model.City;
                        getlocation.Neighborhood = model.Neighborhood;
                        getlocation.Street = model.Street;
                        getlocation.Governorate = model.Governorate;
                        model.Acc_name = _context.Users.FirstOrDefault(x => x.Id == userID).Name;
                        model.Userimage = _context.Users.FirstOrDefault(x => x.Id == userID).Buss_img;
                        _context.SaveChanges();
                        return View(model);
                    }



                }
                catch (Exception)
                {

                }
            }
            return View(model);
        }


        public ActionResult CarAds(int page = 1)
        {
            var DealerID = User.Identity.GetUserId();
            var ShowroomID = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).ID;

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
            }).Where(x => x.UserID == DealerID).ToList();

            List<locations_vm> Branches = _context.locations.Select(x => new locations_vm
            {
                ShoowroomId = x.ShoowroomId,
                Governorate = x.Governorate,
                City = x.City,
                Street = x.Street,
                Neighborhood = x.Neighborhood,
            }).Where(x => x.ShoowroomId == ShowroomID).ToList();
/*
            List<BrandsFilter> BrandsFilter = _context.Cars.Select(x => new BrandsFilter
            {

                Brand = x.Brand,
                ISselected = false
            }).Distinct().ToList();*/

            var model = new DealerVM
            {
                Cars = Cars,
                Branches = Branches,
           /*     BrandsFilter = BrandsFilter,*/
                TraderName = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).TraderName,
                ShowroomName = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).ShowroomName,
                PhoneNumber0 = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).PhoneNumber0,
                PhoneNumber1 = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).PhoneNumber1,
                Email = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).Email,
                Discription = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).Discription,
                Logo = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).Logo,
                CoverImage = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).CoverImage,

                Sizepage = 4,
                car_Data = Cars.OrderBy(d => d.ID),
                CurrentPage = page,


            };


            return View(model);

        }


        public ActionResult Edit(int id)
        {
            var foundID = _context.Cars.Find(id);
            if (foundID == null)
            {

                return RedirectToAction("notFound", "home");
            }
            try
            {
                var car_Brand = _context.Brands_Models.Select(c => c.Brand).Distinct().ToList();
                ViewBag.Brand = car_Brand;
                var governorates = _context.Governorate.Select(c => new
                {
                    GatID = c.id,
                    GatName = c.governorate_name_en
                }).ToList();
                ViewBag.governorates = new MultiSelectList(governorates, "GatID", "GatName");
                ViewBag.Colors = new Colors().getcolors();

                var listoffearureID = _context.carFeatures.Where(p => p.CarID == id).Select(p => p.FeatureID).ToList();
                List<FeatureSelection> featureSelection = _context.features.Select(x => new FeatureSelection
                {
                    FeaturesID = x.ID,
                    Features = x.Feature,
                    ISselected = listoffearureID.Contains(x.ID) ? true : false
                }).ToList();
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
                    Used = x.Used,
                    Discription = x.Discription,
                    Frontimage = _context.carImages.FirstOrDefault(c => c.CarID == id).Frontimage,
                    Backimage = _context.carImages.FirstOrDefault(c => c.CarID == id).Backimage,
                    RightSideimage = _context.carImages.FirstOrDefault(c => c.CarID == id).RightSideimage,
                    LeftSideimage = _context.carImages.FirstOrDefault(c => c.CarID == id).LeftSideimage,
                    Year = x.Year,
                    Model = x.Model,
                    Price = x.Price,

                }).Where(c => c.ID == id).FirstOrDefault();

                return View(userCar);
            }
            catch (Exception)
            {
                return RedirectToAction("error");
                throw;
            }



        }


        [HttpPost]
        public ActionResult Edit(int id, Cars cars, CarImages carImages, HttpPostedFileBase image1, HttpPostedFileBase image2, HttpPostedFileBase image3, HttpPostedFileBase image4)
        {
            var foundID = _context.Cars.Find(id);
            if (foundID == null)
            {

                return RedirectToAction("notFound", "home");
            }
            try
            {
                var car_Brand = _context.Brands_Models.Select(c => c.Brand).Distinct().ToList();
                ViewBag.Brand = car_Brand;
                var governorates = _context.Governorate.Select(c => new
                {
                    GatID = c.id,
                    GatName = c.governorate_name_en
                }).ToList();
                ViewBag.governorates = new MultiSelectList(governorates, "GatID", "GatName");
                ViewBag.Colors = new Colors().getcolors();
                var cars1 = _context.Cars.FirstOrDefault(c => c.ID == id);
                var carImages1 = _context.carImages.FirstOrDefault(c => c.CarID == id);
                cars1.ID = cars.ID;
                cars1.UserID = cars.UserID;
                cars1.Brand = cars.Brand;
                cars1.Body = cars.Body;
                cars1.Fuel = cars.Fuel;
                cars1.Engine_CC = cars.Engine_CC;
                cars1.City = cars.City;
                cars1.Transmission = cars.Transmission;
                cars1.Kilometers_Driven = cars.Kilometers_Driven;
                cars1.Fabrica = cars.Fabrica;
                cars1.Discription = cars.Discription;
                cars1.Year = cars.Year;
                cars1.Model = cars.Model;
                cars1.Price = cars.Price;
                cars1.Color = cars.Color;
                cars1.Used = cars.Used;

                if (image1 != null && image1.ContentLength > 0)
                {

                    string ImageFileName1 = Path.GetFileName(image1.FileName);
                    string FolderPath1 = Path.Combine(Server.MapPath("~/img/Car_Image"), ImageFileName1);
                    image1.SaveAs(FolderPath1);
                    carImages1.Frontimage = ImageFileName1;

                }
                carImages.Frontimage = carImages1.Frontimage;
                if (image2 != null && image2.ContentLength > 0)
                {

                    string ImageFileName2 = Path.GetFileName(image2.FileName);
                    string FolderPath2 = Path.Combine(Server.MapPath("~/img/Car_Image"), ImageFileName2);
                    image2.SaveAs(FolderPath2);

                    carImages1.Backimage = ImageFileName2;

                }
                carImages.Frontimage = carImages1.Backimage;
                if (image3 != null && image3.ContentLength > 0)
                {
                    string ImageFileName3 = Path.GetFileName(image3.FileName);
                    string FolderPath3 = Path.Combine(Server.MapPath("~/img/Car_Image"), ImageFileName3);
                    image3.SaveAs(FolderPath3);

                    carImages1.RightSideimage = ImageFileName3;
                }
                carImages.Frontimage = carImages1.RightSideimage;
                if (image4 != null && image4.ContentLength > 0)
                {
                    string ImageFileName4 = Path.GetFileName(image4.FileName);
                    string FolderPath4 = Path.Combine(Server.MapPath("~/img/Car_Image"), ImageFileName4);
                    image3.SaveAs(FolderPath4);

                    carImages1.LeftSideimage = ImageFileName4;
                }
                carImages.Frontimage = carImages1.LeftSideimage;

                _context.SaveChanges();

                return RedirectToAction("Edit");
            }
            catch (Exception)
            {
                return RedirectToAction("error");
                throw;
            }

        }
        [HttpGet]
        public ActionResult NewBranche()
        {
            var governorates = _context.Governorate.Select(c => new {
                GatID = c.id,
                GatName = c.governorate_name_en
            }).ToList();
            ViewBag.governorates = new MultiSelectList(governorates, "GatID", "GatName");
            var userID = User.Identity.GetUserId();
            var ShowroomData = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == userID);
            if (ShowroomData != null)
            {
                var model = new locations_vm
                {
                    ShowroomName = ShowroomData.ShowroomName,
                    ShowroomDiscription = ShowroomData.Discription,
                    Logo = ShowroomData.Logo,
                    CoverImage = ShowroomData.CoverImage,

                };
                return View(model);
            }
            else
            {
                var model = new locations_vm
                {
                    ShowroomName = "Showroom Name",
                    ShowroomDiscription = "Discription",
                    Logo = "logo.png",
                    CoverImage = "profile-cover.jpg",

                };
                return View(model);
            }
            
        }
        [HttpPost]
        public ActionResult NewBranche(locations_vm Model)
        {
            var governorates = _context.Governorate.Select(c => new {
                GatID = c.id,
                GatName = c.governorate_name_en
            }).ToList();
            ViewBag.governorates = new MultiSelectList(governorates, "GatID", "GatName");
            var userID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                // handel null error 
                var shooroomid = _context.showroomsForTrader.FirstOrDefault(x => x.UserID.Equals(userID)).ID;
                Location NewLocation = new Location();
                NewLocation.Governorate = Model.Governorate;
                NewLocation.City = Model.City;
                NewLocation.Street = Model.Street;
                NewLocation.Neighborhood = Model.Neighborhood;
                NewLocation.ShoowroomId = shooroomid;
                _context.locations.Add(NewLocation);
                _context.SaveChanges();
            }
            var ShowroomData = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == userID);
            if (ShowroomData != null)
            {
                var model = new locations_vm
                {
                    ShowroomName = ShowroomData.ShowroomName,
                    ShowroomDiscription = ShowroomData.Discription,
                    Logo = ShowroomData.Logo,
                    CoverImage = ShowroomData.CoverImage,

                };
                return View(model);
            }
            else
            {
                var model = new locations_vm
                {
                    ShowroomName = "Showroom Name",
                    ShowroomDiscription = "Discription",
                    Logo = "logo.png",
                    CoverImage = "profile-cover.jpg",

                };
                return View(model);
            }
        }
        public ActionResult Reviews()
        {
            var DealerID = User.Identity.GetUserId();
            var ShowroomID = _context.showroomsForTrader.FirstOrDefault(x => x.UserID == DealerID).ID;
            var reviews = _context.ShowroomReviews.Where(s => s.ShowroomID == ShowroomID).ToList();
            return View(reviews);
        }
        public ActionResult Deletecar(int Id)
        {
            try
            {
                var car = _context.Cars.Find(Id);
                _context.Cars.Remove(car);
                _context.SaveChanges();
                return RedirectToAction("CarAds");
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }

}