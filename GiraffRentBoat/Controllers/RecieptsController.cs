using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GiraffRentBoat.Models;

namespace GiraffRentBoat.Controllers
{
    public class RecieptsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reciepts
        public ActionResult Index()
        {
            return View(db.Reciepts.ToList());
        }

        // GET: Reciepts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reciept reciept = db.Reciepts.Find(id);
            if (reciept == null)
            {
                return HttpNotFound();
            }
            return View(reciept);
        }

        // GET: Reciepts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reciepts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecieptId,BoatNameReciept,BookingNumber,RecieptDate,StartDate,TotalCost")] Reciept reciept)
        {
            if (ModelState.IsValid)
            {
                db.Reciepts.Add(reciept);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reciept);
        }

        // GET: Reciepts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reciept reciept = db.Reciepts.Find(id);
            if (reciept == null)
            {
                return HttpNotFound();
            }
            return View(reciept);
        }

        // POST: Reciepts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecieptId,BoatNameReciept,BookingNumber,RecieptDate,StartDate,TotalCost")] Reciept reciept)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reciept).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reciept);
        }

        // GET: Reciepts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reciept reciept = db.Reciepts.Find(id);
            if (reciept == null)
            {
                return HttpNotFound();
            }
            return View(reciept);
        }

        // POST: Reciepts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reciept reciept = db.Reciepts.Find(id);
            db.Reciepts.Remove(reciept);
            db.SaveChanges();
            return RedirectToAction("Index");
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
