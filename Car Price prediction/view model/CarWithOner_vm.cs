using Sell_your_car.view_model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.view_model
{
    public class CarWithOner_vm
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Body { get; set; }
        public string Transmission { get; set; }
        public int Year { get; set; }
        public string Fuel { get; set; }
        public int Engine_CC { get; set; }
        public int? Kilometers_Driven { get; set; }
        public string Color { get; set; }
        public string City { get; set; } 
        public bool? Used { get; set; } 
        public double Price { get; set; }
        public bool Fabrica { get; set; }
        public string Discription { get; set; }
        public string Frontimage { get; set; }
        public string Backimage { get; set; }
        public string RightSideimage { get; set; }
        public string LeftSideimage { get; set; }
        public string Governorate { get; set; }
        public List<FeatureSelection> CarFeatures { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
/*        public string [] CarImgCollection1 { get; set; }*/
    }
}