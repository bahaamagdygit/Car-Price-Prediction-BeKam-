using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.view_model
{
    public class Colors
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public List<Colors> getcolors()
        {
            List<Colors> cols = new List<Colors>();
            cols.Add(new Colors { ID = 1, Name = "blue", Value = "#007bff" });
            cols.Add(new Colors { ID = 2, Name = "Gray", Value = "#6c757d" });
            cols.Add(new Colors { ID = 3, Name = "White", Value = " " });
            cols.Add(new Colors { ID = 4, Name = "Mocha", Value = "#6f372d" });
            cols.Add(new Colors { ID = 5, Name = "Black", Value = "#000000" });
            cols.Add(new Colors { ID = 6, Name = "Gold", Value = "#f9d77e" });
            cols.Add(new Colors { ID = 7, Name = "Silver", Value = "#bebdb6" });
            cols.Add(new Colors { ID = 8, Name = "Dark red", Value = "#8B0000" });
            cols.Add(new Colors { ID = 9, Name = "Brown", Value = "#35281e" });
            cols.Add(new Colors { ID = 10, Name = "Bronze", Value = "#51412d" });
            cols.Add(new Colors { ID = 11, Name = "Red", Value = "#dc3545" });
            cols.Add(new Colors { ID = 12, Name = "Champagne", Value = "#eed9b6" });
            cols.Add(new Colors { ID = 13, Name = "Darkgreen", Value = "#006400" });
            cols.Add(new Colors { ID = 14, Name = "Lightgrey", Value = "#90ee90" });
            cols.Add(new Colors { ID = 15, Name = "Beige", Value = "#d9b99b" });
            cols.Add(new Colors { ID = 16, Name = "Darkblue", Value = "#00008b" });
            cols.Add(new Colors { ID = 17, Name = "Green", Value = "#245336" });
            cols.Add(new Colors { ID = 18, Name = "Eggplant", Value = "#483248" });
            cols.Add(new Colors { ID = 12, Name = "Cyan", Value = "#00FFFF" });
            cols.Add(new Colors { ID = 20, Name = "Yellow", Value = "#ffc107" });
            cols.Add(new Colors { ID = 21, Name = "Petroleum", Value = "#005f6a" });
            cols.Add(new Colors { ID = 22, Name = "Purple", Value = "#800080" });
            cols.Add(new Colors { ID = 23, Name = "Olive", Value = "#808000" });
            cols.Add(new Colors { ID = 24, Name = "Orange", Value = "#FFA500" });
            return cols;
        }
    }
}