using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.Models
{
    public class user_cars
    {
        public int ID { get; set; }
        public DateTime InsertionDate { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public IdentityUser User { get; set; }

        [ForeignKey("Cars")]
        public int CarID { get; set; }
        public Cars Cars { get; set; }
    }
}