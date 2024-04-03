using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.Models
{
    public class brands_models
    {
        [Key]
        public int ID { get; set; } //
        public string Brand { get; set; }    //
        public string Model { get; set; }   //
    }
}