using Car_Price_prediction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.view_model
{
    public class ShowroomsWithLocation
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Userimage { get; set; }
        public string TraderName { get; set; }
        public string Acc_name  { get; set; }
        public string ShowroomName { get; set; }
        public string PhoneNumber0 { get; set; }
        public string PhoneNumber1 { get; set; }
        public string Email { get; set; }
        public string Discription { get; set; }
        public string Logo { get; set; }
        public string CoverImage { get; set; }

        /*   public List<Location> locations { get; set; }*/
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }


    }
}