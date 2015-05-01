using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EquipmentManager.Data;

namespace EquipmentManager.Controllers
{
    public class UnitsController : Controller
    {
        private EquipmentManagerEntities db = new EquipmentManagerEntities();

        // GET: Units
        public ActionResult Index()
        {
            return View(db.UnitsOfMeasures.ToList());
        }

        // GET: Units/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitsOfMeasure unitsOfMeasure = db.UnitsOfMeasures.Find(id);
            if (unitsOfMeasure == null)
            {
                return HttpNotFound();
            }
            return View(unitsOfMeasure);
        }

        // GET: Units/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Units/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] UnitsOfMeasure unitsOfMeasure)
        {
            if (ModelState.IsValid)
            {
                db.UnitsOfMeasures.Add(unitsOfMeasure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(unitsOfMeasure);
        }

        // GET: Units/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitsOfMeasure unitsOfMeasure = db.UnitsOfMeasures.Find(id);
            if (unitsOfMeasure == null)
            {
                return HttpNotFound();
            }
            return View(unitsOfMeasure);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] UnitsOfMeasure unitsOfMeasure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unitsOfMeasure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(unitsOfMeasure);
        }

        // GET: Units/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitsOfMeasure unitsOfMeasure = db.UnitsOfMeasures.Find(id);
            if (unitsOfMeasure == null)
            {
                return HttpNotFound();
            }
            return View(unitsOfMeasure);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UnitsOfMeasure unitsOfMeasure = db.UnitsOfMeasures.Find(id);
            db.UnitsOfMeasures.Remove(unitsOfMeasure);
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
