namespace GiraffRentBoat.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GiraffRentBoat.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GiraffRentBoat.Models.ApplicationDbContext context)
        {
            var boats = new[]
            {
                new Boat {  Id=1, BoatName="Ferry",BoatActive = false, BookingNumber="ABC123", BoatType= "Segelbåt < 40 fot" },
                new Boat {  Id=2, BoatName="Yatch",BoatActive = true, BookingNumber="XYZ123", BoatType= "Segelbåt > 40 fot" },
                new Boat {  Id=3, BoatName="Jolle",BoatActive = false, BookingNumber="AZX123", BoatType= "Jolle" },
            };
            context.Boats.AddOrUpdate(boats);
            context.SaveChanges();

            //var booking = new[]
            //{
            //    new Booking { BookingId=1,/*Active=false, */ BoatId=1},
            //    new Booking { BookingId=2,/*Active=false, */BoatId=2},
            //    new Booking { BookingId=3,/*Active=false, */ BoatId=3},
            //};
            //context.Bookings.AddOrUpdate(booking);
            //context.SaveChanges();
        }
    }
}
