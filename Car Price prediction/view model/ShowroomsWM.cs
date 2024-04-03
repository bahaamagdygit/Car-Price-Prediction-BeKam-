using Car_Price_prediction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.view_model
{
    public class ShowroomsWM
    {
        public int ID { get; set; }
        public List<ShowroomsForTrader> Showrooms { get; set; }

        public IEnumerable<ShowroomsForTrader> Showroom_Data { get; set; }

        public int Sizepage { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount()
        {

            return Convert.ToInt32(Math.Ceiling(Showroom_Data.Count() / (double)Sizepage));

        }
        public int pagestart()
        {
            if (CurrentPage > 9)
            {
                int x;
                x = CurrentPage - 9;
                return x;
            }
            return 1;

        }
        public int pagestart1()
        {
            if (pagestart() > 9)
            {

                return pagestart();
            }
            return 9;
        }
        public int Previous()
        {
            if (CurrentPage == 1)
            {
                return 1;
            }
            else
                return CurrentPage - 1;
        }
        public int Next()
        {
            if (CurrentPage == PageCount())
            {
                return CurrentPage;
            }
            else
                return CurrentPage + 1;

        }


        public IEnumerable<ShowroomsForTrader> PaginatedShowroom_Data()
        {

            int start = ((CurrentPage - 1) * Sizepage);
            return Showroom_Data.OrderBy(cd => cd.ID).Skip(start).Take(Sizepage);
        }
    }
}