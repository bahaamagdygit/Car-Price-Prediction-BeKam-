using Car_Price_prediction.Models;
using Car_Price_prediction.view_model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_your_car.view_model
{
    public class SellYourCarViewModel     {
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
        public double Price { get; set; }
        public bool Fabrica { get; set; }
        public string Discription { get; set; }
        public string Frontimage { get; set; }
        public string Backimage { get; set; }
        public string RightSideimage { get; set; }
        public string LeftSideimage { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Governorate { get; set; }
        public List<FeatureSelection> CarFeatures { get; set; }
        public List<Colors> colors { get; set; }

        public string TraderName { get; set; }
        public string ShowroomName { get; set; }
        public string ShowroomPhoneNumber0 { get; set; }
        public string ShowroomPhoneNumber1 { get; set; }
        public string ShowroomEmail { get; set; }
        public string ShowroomDiscription { get; set; }
        public string Logo { get; set; }
        public string CoverImage { get; set; }
    }
}