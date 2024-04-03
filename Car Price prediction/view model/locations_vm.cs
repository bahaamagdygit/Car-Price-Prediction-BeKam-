using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.view_model
{
    public class locations_vm
    {
        public int ShoowroomId { get; set; }
        [Required(ErrorMessage = "Please Select Governorate")]
        public string Governorate { get; set; }
        [Required(ErrorMessage = "Please Select City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please Enter Street name"), MaxLength(30)]
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string CoverImage { get; set; }
        public string Logo { get; set; }
        public string ShowroomName { get; set; }
        public string ShowroomDiscription { get; set; }
    }
}