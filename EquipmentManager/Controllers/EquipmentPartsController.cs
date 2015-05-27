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
    public class EquipmentPartsController : Controller
    {
        private EquipmentManagerEntities db = new EquipmentManagerEntities();

        // GET: EquipmentParts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentPart EquipmentPart = db.EquipmentParts.Find(id);
            if (EquipmentPart == null)
            {
                return HttpNotFound();
            }
            return View(EquipmentPart);
        }

        // GET: EquipmentParts/Create
        public ActionResult Create()
        {
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name");
            ViewBag.ItemId = new SelectList(db.Parts, "Id", "Name");
            return View();
        }

        // POST: EquipmentParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,EquipmentId,ItemId,QuantityRequired,QuantityRequiredSpare,UnitOfMeasure,Notes,ValidFrom,ValidTo")] EquipmentPart EquipmentPart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.EquipmentParts.Add(EquipmentPart);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", EquipmentPart.EquipmentId);
        //    ViewBag.ItemId = new SelectList(db.Parts, "Id", "Name", EquipmentPart.ItemId);
        //    return View(EquipmentPart);
        //}

        // GET: EquipmentParts/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EquipmentPart EquipmentPart = db.EquipmentParts.Find(id);
        //    if (EquipmentPart == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", EquipmentPart.EquipmentId);
        //    ViewBag.ItemId = new SelectList(db.Parts, "Id", "Name", EquipmentPart.ItemId);
        //    return View(EquipmentPart);
        //}

        // POST: EquipmentParts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,EquipmentId,ItemId,QuantityRequired,QuantityRequiredSpare,UnitOfMeasure,Notes,ValidFrom,ValidTo")] EquipmentPart EquipmentPart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(EquipmentPart).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", EquipmentPart.EquipmentId);
        //    ViewBag.ItemId = new SelectList(db.Parts, "Id", "Name", EquipmentPart.ItemId);
        //    return View(EquipmentPart);
        //}

        // GET: EquipmentParts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentPart EquipmentPart = db.EquipmentParts.Find(id);
            if (EquipmentPart == null)
            {
                return HttpNotFound();
            }
            return View(EquipmentPart);
        }

        // POST: EquipmentParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipmentPart EquipmentPart = db.EquipmentParts.Find(id);
            db.EquipmentParts.Remove(EquipmentPart);
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
