using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.Models
{
    public class CarFeatures
    {
        public int ID { get; set; }
        [ForeignKey("Features")]
        public int FeatureID { get; set; }
        public Features Features { get; set; }
        [ForeignKey("Cars")]
        public int CarID { get; set; }
        public Cars Cars { get; set; }
    }
}