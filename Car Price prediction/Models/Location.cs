using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }

        [ForeignKey("ShowroomsForTrader")]
        public int ShoowroomId { get; set; }
        public ShowroomsForTrader ShowroomsForTrader { get; set; }

    }
}