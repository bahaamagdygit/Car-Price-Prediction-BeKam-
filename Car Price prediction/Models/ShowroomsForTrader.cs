using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.Models
{
    public class ShowroomsForTrader
    {
        public int ID { get; set; }
        public string TraderName { get; set; }
        public string ShowroomName { get; set; }
        public string PhoneNumber0 { get; set; }
        public string PhoneNumber1 { get; set; }
        public string Email { get; set; }
        public string Discription { get; set; }
        public string Logo { get; set; }
        public string CoverImage { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public IdentityUser User { get; set; }
    }
}