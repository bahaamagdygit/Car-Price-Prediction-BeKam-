using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.Models
{
    public class CarImages
    {
        public int ID { get; set; }

        public string Frontimage { get; set; }
        public string Backimage { get; set; }
       
        public string RightSideimage { get; set; }
        public string LeftSideimage { get; set; }
        [ForeignKey("Cars")]
        public int CarID { get; set; }
        public Cars Cars { get; set; }
    }
}