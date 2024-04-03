using Car_Price_prediction.Models;
using Car_Price_prediction.view_model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace Car_Price_prediction.Controllers
{
    public class Car_finderController : Controller
    {
        ApplicationDbcontext _context = new ApplicationDbcontext();
        
        public ActionResult Cars_filter(int page=1)
        {
            var car_model_year = _context.Cars.Select(c => c.Year).Distinct().ToList();
            ViewBag.year = new MultiSelectList(car_model_year);

           /* var car_model_price = _context.Cars.Select(c => c.Price).Distinct().ToList();
            ViewBag.Price = new MultiSelectList(car_model_price);*/

            var car_Engine_CC = _context.Cars.Select(c => c.Engine_CC).Distinct().ToList();
            ViewBag.car_Engine_CC = new MultiSelectList(car_Engine_CC);
            // prepare vm to be passsed to the view 
            // get car list from db
            List<car_detials_vm> cars = _context.Cars.Select(x => new car_detials_vm
            {
                ID = x.ID,
                UserID =x.UserID,
                Brand = x.Brand,
                Body = x.Body,
                Fuel = x.Fuel,
                Engine_CC_Range = x.Engine_CC,
                Color = x.Color,
                City = x.City,
                Transmission = x.Transmission,
                Kilometers_Driven = x.Kilometers_Driven,
                yearRange = x.Year,
                PriceRange = x.Price,
                image = _context.carImages.FirstOrDefault(v => v.CarID == x.ID) != null ?
                _context.carImages.FirstOrDefault(v => v.CarID == x.ID).Frontimage.ToString() : null,

            }).ToList();

            // get Brand selection list
            List<MakerSelection> BrandList = _context.Cars.Select(x => new MakerSelection
            {
                Brand = x.Brand,
                ISselected = false
            }).Distinct().ToList();
            // get Size selection list
    
            var model = new CarListVM
            {
                Dealars = _context.showroomsForTrader.Select(x => new DealerVM
                {
                    UserID = x.UserID,
                }).ToList(),
                Cars = cars,
     
                Selections_FOR_MAKE = BrandList,
               
                Sizepage = 12,
                car_Data = cars.OrderBy(d => d.ID),
                CurrentPage = page,
                Price0 = 0,
                Price1 = 3500000.0,
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Cars_filter(CarListVM model, int page = 1)
        {
            var car_model_year = _context.Cars.Select(c => c.Year).Distinct().ToList();
            ViewBag.year = new MultiSelectList(car_model_year);

            var car_model_price = _context.Cars.Select(c => c.Price).Distinct().ToList();
            ViewBag.Price = new MultiSelectList(car_model_price);

            var car_Engine_CC = _context.Cars.Select(c => c.Engine_CC).Distinct().ToList();
            ViewBag.car_Engine_CC = new MultiSelectList(car_Engine_CC);

            // get the selected Brand options say bmw and oudi
            var selectedoptions_for_Brand = model.Selections_FOR_MAKE.Where(x => x.ISselected == true).Select(x => x.Brand).ToList();
            // get the selected size  options say larg and compact
         
            // get car list from db
            model.Cars = _context.Cars.Select(x => new car_detials_vm
            {
                ID = x.ID,
                Brand = x.Brand,
                Body = x.Body,
                Fuel = x.Fuel,
                Engine_CC_Range = x.Engine_CC,
                Color = x.Color,
                City = x.City,
                Transmission = x.Transmission,
                Kilometers_Driven = x.Kilometers_Driven,
              /*  Fabrica = x.Fabrica,*/
                yearRange = x.Year,
                PriceRange = x.Price,
                image = _context.carImages.FirstOrDefault(v => v.CarID == x.ID) != null ?
                _context.carImages.FirstOrDefault(v => v.CarID == x.ID).Frontimage.ToString() : null,
            }).Where(c => selectedoptions_for_Brand.Contains(c.Brand) || selectedoptions_for_Brand.Count == 0)
                   .Where(x => x.yearRange >= model.year0 && x.yearRange <= model.year1 || x.yearRange >= model.year0 && model.year1 == null
                   || x.yearRange <= model.year1 && model.year0 == null || model.year0 == null && model.year1 == null)
                   .Where(x => x.PriceRange >= model.Price0 && x.PriceRange <= model.Price1 || model.Price0 == null || model.Price1 == null)
                   .Where(e => e.Fuel == model.Fuel || model.Fuel == null || model.Fuel == "")
                   .Where(e => e.Transmission == model.Transmission || model.Transmission == null || model.Transmission == "")
                   .Where(e => e.Engine_CC_Range >= model.Engine_CC0 && e.Engine_CC_Range <= model.Engine_CC1 || e.Engine_CC_Range >= model.Engine_CC0 && model.Engine_CC1 == null ||
                    e.Engine_CC_Range <= model.Engine_CC1 && model.Engine_CC0 == null || model.Engine_CC0 == null && model.Engine_CC1 == null)
                   .Where(n => n.City == model.City || model.City== null)
                   .Where(n => n.Body == model.Body || model.Body== null).ToList();

            // get Brand selection list
            List<MakerSelection> BrandList = _context.Cars.Select(x => new MakerSelection
            {
                Brand = x.Brand,
                ISselected = false
            }).Distinct().ToList();
  

            var Carss = new CarListVM
            {
                Dealars = _context.showroomsForTrader.Select(x => new DealerVM
                {
                    UserID = x.UserID,
                }).ToList(),
                Cars = model.Cars,
                Selections_FOR_MAKE = BrandList,
               
                Sizepage = 12,
                car_Data = model.Cars.OrderBy(d => d.ID),
                CurrentPage = page,
                Price0 = model.Price0,
                Price1 = model.Price1,
            };
            return View(Carss);
        }

        public ActionResult Carpricepredict()
        {
            return View();
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
                ViewBag.img = _context.carImages.FirstOrDefault(v => v.CarID == id) != null ?
                 _context.carImages.FirstOrDefault(v => v.CarID == id).Frontimage.ToString() : null;
             
                
                Cars car =
                _context.Cars.FirstOrDefault(v => v.ID == id) != null ?
                   _context.Cars.FirstOrDefault(v => v.ID == id) : null;
                return View(car);
            }
            catch (Exception)
            {
                return RedirectToAction("error");
                throw;
            }
            
        
        }
    }
}