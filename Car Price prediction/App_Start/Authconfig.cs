using Car_Price_prediction.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Car_Price_prediction
{

    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public int age { get; set; }
        public string Buss_img { get; set; }
    }
    public  class ApplicationDbcontext : IdentityDbContext  <ApplicationUser>
    {
       public ApplicationDbcontext () : base("Car_Price_PredictionEntities")
        {
             
        } 
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Features> features { get; set; }
        public DbSet<CarFeatures> carFeatures { get; set; }
        public DbSet<brands_models> Brands_Models { get; set; } 
        public DbSet<CustomerMessage> CustomerMessages { get; set; } 
        public DbSet<CarImages> carImages { get; set; } 
        public DbSet<city> Cities { get; set; } 
        public DbSet<Governorate> Governorate { get; set; } 
        public DbSet<ShowroomsForTrader> showroomsForTrader{ get; set; } 
        public DbSet<Location> locations{ get; set; } 
        public DbSet<ShowroomReviews> ShowroomReviews { get; set; } 
        public DbSet<user_cars> user_cars { get; set; } 

    }
}