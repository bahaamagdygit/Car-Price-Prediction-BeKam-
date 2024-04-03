using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sell_your_car.view_model
{

    public class FeatureSelection
    {
        public int FeaturesID { get; set; }
        public string Features { get; set; }
        public bool ISselected { get; set; }
        public FeatureSelection()
        {

        }
    }
}