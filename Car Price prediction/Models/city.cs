using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.Models
{
    public class city
    {
        public int id { get; set; }
        public string city_name_en { get; set; }

        [ForeignKey("Governorate")]
        public int governorateID { get; set; }
        public Governorate Governorate { get; set; }
    }
}