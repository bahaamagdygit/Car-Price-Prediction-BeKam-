using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.view_model
{
    public class car_detials_vm
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int? year0 { get; set; }
        public int? year1 { get; set; }
        public int? yearRange { get; set; }
        public string Fuel { get; set; }
        public double? Engine_CC0 { get; set; }
        public double? Engine_CC1 { get; set; }
        public double? Engine_CC_Range { get; set; }
        public string Transmission { get; set; }

        public string Color { get; set; }
        public string City { get; set; }
        public int? Kilometers_Driven { get; set; }
        public string Governorate { get; set; }
        public decimal? Number_of_Doors { get; set; }
        public string Body { get; set; }
        public double? Price0 { get; set; }
        public double? Price1 { get; set; }
        public bool Fabrica { get; set; }
        public string UserName { get; set; }
        public string Phone_Namber { get; set; }
        public double? PriceRange { get; set; }
        public string image { get; set; }
        public string Frontimage { get; set; }
        public string Backimage { get; set; }
        public string RightSideimage { get; set; }
        public string LeftSideimage { get; set; }
        public bool Active { get; set; }
        public String UserID { get; set; }
    }
}