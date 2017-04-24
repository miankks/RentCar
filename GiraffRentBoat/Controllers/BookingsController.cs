using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GiraffRentBoat.Models;
using System.Web.UI;

namespace GiraffRentBoat.Controllers
{

    public class BookingsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        //Outputcache for 300 seconds
        [OutputCache(Duration = 300, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Boat);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        //Cache for 20 seconds
        [OutputCache(Duration = 20, VaryByParam = "none")]
        public ActionResult Create(bool? active, int? boatid)
        {
            var boatActive = db.Boats.Find(boatid);
            boatActive.BoatActive = true;
            if (active == false)
            {
                ViewBag.BoatId = new SelectList(db.Boats, "Id", "BoatName");
                return View();
            }
            else
            {
                TempData["errormessage"] = "Båt är redan bokat";
                return RedirectToAction("Index");
            }
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,Personnummer,StartTime,EndTime,Active,BoatId")] Booking bookings, int? boatId)
        {
            var boat = new Boat();
            var boatActive = db.Boats.Where(x => x.Id == boatId).Select(x => x.BoatActive).SingleOrDefault();
            boat.BoatActive = true;
         
            if (ModelState.IsValid)
                {
                        db.Bookings.Add(bookings);
                    db.SaveChanges();
                    return View("Booked");
                }
            

            ViewBag.BoatId = new SelectList(db.Boats, "Id", "BoatName", bookings.BoatId);
            return View(bookings);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoatId = new SelectList(db.Boats, "Id", "BoatName", booking.BoatId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,Personnummer,StartTime,EndTime,Active,BoatId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BoatId = new SelectList(db.Boats, "Id", "BoatName", booking.BoatId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id, int boatId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? boatid)
        {
            var boatname = db.Boats.Find(boatid);
            var reciept = new Reciept();
            Booking booking = db.Bookings.Find(id);

            //Iteration to select boattype for camparison later in this ActionResult
            var boattype = db.Boats.Where(x => x.Id == boatid).Select(x => x.BoatType).SingleOrDefault().ToString();
            var currenttime = (DateTime.Now - booking.StartTime).TotalHours;

            //compare boat sizes to less than 40 feet, greater then 40 feet, or else just a name category
            if (boattype == "Segelbåt < 40 fot")
            {
                reciept.TotalCost = Math.Round(((200 * 1.2) + ((100 * 1.3) * currenttime)));
            }
            else if (boattype == "Segelbåt > 40 fot")
            {
                reciept.TotalCost = Math.Round(((200 * 1.5) + ((100 * 1.4) * currenttime)));
            }
            else
            {
                reciept.TotalCost = Math.Round((200 + (100 * currenttime)));
            }
            reciept.BoatNameReciept = boatname.BoatName;
            reciept.BookingNumber = boatname.BookingNumber;
            reciept.StartDate = booking.StartTime;
            reciept.RecieptDate = DateTime.Now;

            //Save for future analysis
            db.Reciepts.Add(reciept);
            db.SaveChanges();
            boatname.BoatActive = false;
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return View("Reciept", reciept);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
