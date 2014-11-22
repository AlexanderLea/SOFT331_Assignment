using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOFT331_Assignment.Models;

namespace SOFT331_Assignment.Controllers
{
    public class TravellerController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: /Traveller/
        public ActionResult Index()
        {
            return View(db.Travellers.ToList());
        }

        // GET: /Traveller/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Traveller traveller = db.Travellers.Find(id);
            if (traveller == null)
            {
                return HttpNotFound();
            }
            return View(traveller);
        }

        // GET: /Traveller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Traveller/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TravellerID,FirstName,LastName,Address1,PostCode")] Traveller traveller)
        {
            if (ModelState.IsValid)
            {
                db.Travellers.Add(traveller);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(traveller);
        }

        // GET: /Traveller/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Traveller traveller = db.Travellers.Find(id);
            if (traveller == null)
            {
                return HttpNotFound();
            }
            return View(traveller);
        }

        // POST: /Traveller/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TravellerID,FirstName,LastName,Address1,PostCode")] Traveller traveller)
        {
            if (ModelState.IsValid)
            {
                db.Entry(traveller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(traveller);
        }

        // GET: /Traveller/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Traveller traveller = db.Travellers.Find(id);
            if (traveller == null)
            {
                return HttpNotFound();
            }
            return View(traveller);
        }

        // POST: /Traveller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Traveller traveller = db.Travellers.Find(id);
            db.Travellers.Remove(traveller);
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
