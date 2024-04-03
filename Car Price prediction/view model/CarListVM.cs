using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Car_Price_prediction.Models;
namespace Car_Price_prediction.view_model
{
    public class CarListVM
    {
        public List<car_detials_vm> Cars { get; set; }
        public List<MakerSelection> Selections_FOR_MAKE{ get; set; }
        public List<DealerVM> Dealars { get; set; }
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Body { get; set; }
        public string UserName { get; set; }
        public int? year0 { get; set; }
        public int? year1 { get; set; }
        public int? yearRange { get; set; }
        public string Fuel { get; set; }
        public double? Engine_CC0 { get; set; }
        public double? Engine_CC1 { get; set; }
        public double? Engine_CC_Range { get; set; }
        public string Transmission { get; set; }
        public int? Kilometers_Driven { get; set; }
        public string Color { get; set; }
        public string City { get; set; }
        public bool Fabrica { get; set; }
        public string Discription { get; set; }
        public double? Price0 { get; set; }
        public double? Price1 { get; set; }
        public double? PriceRange { get; set; }
        public string image { get; set; }
        public string Governorate { get; set; }
        public IEnumerable<car_detials_vm> car_Data { get; set; }
        public int Sizepage { get; set; }
        public int CurrentPage { get; set; }
        //public int Previous { get; set; }
        //public int Next { get; set; }
        public int PageCount()
        {
            
            return Convert.ToInt32(Math.Ceiling(car_Data.Count() / (double)Sizepage));
           
        }
        public int pagestart()
        {
            if (CurrentPage > 4)
            {
                int x;
                x = CurrentPage - 4;
                return x;
            }
            return 1;

        }
        public int pagestart1()
        {
            if (pagestart() > 4)
            {

                return pagestart();
            }
            return 4;
        }
        public int Previous()
        {
            if (CurrentPage ==1)
            {
                return 1;
            }
            else
            return CurrentPage - 1;
        } 
        public int Next()
        {
            if (CurrentPage==PageCount())
            {
                return CurrentPage;
            }
            else
            return CurrentPage  + 1;

        }
        
      
            public IEnumerable<car_detials_vm> Paginatedcar_data()
              { 

              int start = ((CurrentPage - 1) * Sizepage);
              return car_Data.OrderBy(cd => cd.ID).Skip(start).Take(Sizepage);        
              }
    }

}