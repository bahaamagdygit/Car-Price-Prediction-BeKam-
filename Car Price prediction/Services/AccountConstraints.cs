using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Car_Price_prediction.Services
{
    public class AccountConstraints
    {
        ApplicationDbcontext _context = new ApplicationDbcontext();

        public void calendar()
        {

            ApplicationDbcontext _Context = new ApplicationDbcontext();
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(_Context);
            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);

            // Get the current date and time
            var currentDate = DateTime.Now.Date;

            // Get all UserId from user_cars Table
            var userIds = _context.user_cars.Select(uc => uc.UserID).ToList();

            var carsToUpdate = new List<int>();
            var carsToRemove = new List<int>();

            foreach (var userId in userIds)
            {
                var roleName = manager.GetRoles(userId);
                var maxAllowedDate = currentDate.AddDays(roleName.Contains("Dealer") ? -30 : -15);

                var userCarsToUpdate = _context.user_cars
                    .Where(uc => uc.UserID.Equals(userId) && uc.InsertionDate <= maxAllowedDate)
                    .Select(uc => uc.CarID)
                    .ToList();

                carsToUpdate.AddRange(userCarsToUpdate);
                carsToRemove.AddRange(_context.user_cars.Where(uc => uc.UserID.Equals(userId) && uc.InsertionDate <= maxAllowedDate).Select(uc => uc.ID));
            }

            // Update the Active column for the matching cars in a single batch update
            _context.Cars.Where(c => carsToUpdate.Contains(c.ID)).ToList().ForEach(c => c.Active = false);

            // Remove user cars that exceed the allowed date range in a single batch remove
            _context.user_cars.RemoveRange(_context.user_cars.Where(uc => carsToRemove.Contains(uc.ID)));

            _context.SaveChanges();
        }

        //public int  CarSellsCount(String userID)
        //{
        //    var currentDate = DateTime.Now.Date;
        //    var maxAllowedDate = currentDate.AddDays(-30);

        //    int car_count = _context.user_cars
        //        .Where(c => c.UserID == userID && DbFunctions.TruncateTime(c.InsertionDate) <= maxAllowedDate).Count();

        //    return   car_count;


        //}
    }
}