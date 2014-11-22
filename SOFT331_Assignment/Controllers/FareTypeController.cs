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
    public class FareTypeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: /FareType/
        public ActionResult Index()
        {
            return View(db.FareTypes.ToList());
        }

        // GET: /FareType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FareType faretype = db.FareTypes.Find(id);
            if (faretype == null)
            {
                return HttpNotFound();
            }
            return View(faretype);
        }

        // GET: /FareType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /FareType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="FaretypeID,FareTypeDescription,BasicPrice,GiftAidPrice")] FareType faretype)
        {
            if (ModelState.IsValid)
            {
                db.FareTypes.Add(faretype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(faretype);
        }

        // GET: /FareType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FareType faretype = db.FareTypes.Find(id);
            if (faretype == null)
            {
                return HttpNotFound();
            }
            return View(faretype);
        }

        // POST: /FareType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="FaretypeID,FareTypeDescription,BasicPrice,GiftAidPrice")] FareType faretype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faretype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faretype);
        }

        // GET: /FareType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FareType faretype = db.FareTypes.Find(id);
            if (faretype == null)
            {
                return HttpNotFound();
            }
            return View(faretype);
        }

        // POST: /FareType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FareType faretype = db.FareTypes.Find(id);
            db.FareTypes.Remove(faretype);
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
