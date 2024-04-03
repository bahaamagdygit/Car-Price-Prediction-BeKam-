using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.Models
{
    public class Cars 
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Body { get; set; }
        public string Transmission { get; set; }
        public int Year { get; set; }
        public string Fuel { get; set; }
        public bool ? Used { get; set; }
        public int Engine_CC { get; set; }
        public int? Kilometers_Driven { get; set; }
        public string Color { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public bool Fabrica { get; set; }
        public string Discription { get; set; }
        public bool Active { get; set; } = true;

        public IEnumerable<Features> Features { get; set; }


        [ForeignKey("User")]
        public string UserID { get; set; }
        public IdentityUser User { get; set; }

  


    }

 /*   public enum Colors
    {
        Blue ,
        Gray ,
        White,
        Mocha, 
        Black,
        Gold,
        Silver,
        Darkred, 
        Brown,
        Bronze,
        Red,
        Champagne,
        Darkgreen,
        Lightgrey,
        Beige,
        Darkblue, Green, Eggplant, Cyan,
        Yellow, Petroleum, Purple, Olive, Orange
    }*/

    
}