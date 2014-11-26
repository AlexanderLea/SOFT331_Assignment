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
    public class JourneyTypesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: JourneyTypes
        public ActionResult Index()
        {
            return View(db.JourneyTypes.ToList());
        }

        // GET: JourneyTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JourneyType journeyType = db.JourneyTypes.Find(id);
            if (journeyType == null)
            {
                return HttpNotFound();
            }
            return View(journeyType);
        }

        // GET: JourneyTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JourneyTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JourneyTypeID,JourneyTypeName")] JourneyType journeyType)
        {
            if (ModelState.IsValid)
            {
                db.JourneyTypes.Add(journeyType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(journeyType);
        }

        // GET: JourneyTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JourneyType journeyType = db.JourneyTypes.Find(id);
            if (journeyType == null)
            {
                return HttpNotFound();
            }
            return View(journeyType);
        }

        // POST: JourneyTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JourneyTypeID,JourneyTypeName")] JourneyType journeyType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(journeyType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(journeyType);
        }

        // GET: JourneyTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JourneyType journeyType = db.JourneyTypes.Find(id);
            if (journeyType == null)
            {
                return HttpNotFound();
            }
            return View(journeyType);
        }

        // POST: JourneyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JourneyType journeyType = db.JourneyTypes.Find(id);
            db.JourneyTypes.Remove(journeyType);
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
