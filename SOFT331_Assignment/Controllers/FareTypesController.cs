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
    public class FareTypesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: FareTypes
        public ActionResult Index()
        {
            return View(db.FareTypes.ToList());
        }

        // GET: FareTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FareType fareType = db.FareTypes.Find(id);
            if (fareType == null)
            {
                return HttpNotFound();
            }
            return View(fareType);
        }

        // GET: FareTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FareTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FaretypeID,FareTypeDescription")] FareType fareType)
        {
            if (ModelState.IsValid)
            {
                db.FareTypes.Add(fareType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fareType);
        }

        // GET: FareTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FareType fareType = db.FareTypes.Find(id);
            if (fareType == null)
            {
                return HttpNotFound();
            }
            return View(fareType);
        }

        // POST: FareTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FaretypeID,FareTypeDescription")] FareType fareType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fareType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fareType);
        }

        // GET: FareTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FareType fareType = db.FareTypes.Find(id);
            if (fareType == null)
            {
                return HttpNotFound();
            }
            return View(fareType);
        }

        // POST: FareTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FareType fareType = db.FareTypes.Find(id);
            db.FareTypes.Remove(fareType);
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
