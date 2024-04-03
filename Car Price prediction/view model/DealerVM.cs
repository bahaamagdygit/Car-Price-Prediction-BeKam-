using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.view_model
{
    public class DealerVM
    {
        public List<car_detials_vm> Cars { get; set; }
        public List<locations_vm> Branches { get; set; }
        public List<BrandsFilter> BrandsFilter { get; set; }

        public int ID { get; set; }
        public string TraderName { get; set; }
        public string ShowroomName { get; set; }
        public string PhoneNumber0 { get; set; }
        public string PhoneNumber1 { get; set; }
        public string Email { get; set; }
        public string Discription { get; set; }
        public string Logo { get; set; }
        public string CoverImage { get; set; }
        public int ShowroomID { get; set; }
        public string UserID { get; set; }
        public IEnumerable<car_detials_vm> car_Data { get; set; }

        public int Sizepage { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount()
        {

            return Convert.ToInt32(Math.Ceiling(car_Data.Count() / (double)Sizepage));

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


        public IEnumerable<car_detials_vm> Paginatedcar_data()
        {

            int start = ((CurrentPage - 1) * Sizepage);
            return car_Data.OrderBy(cd => cd.ID).Skip(start).Take(Sizepage);
        }
    }
}
public class BrandsFilter
{
    public string Brand { get; set; }
    public bool ISselected { get; set; }
}