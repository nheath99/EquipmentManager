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
    public class EquipmentItemsController : Controller
    {
        private EquipmentManagerEntities db = new EquipmentManagerEntities();

        // GET: EquipmentItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentItem equipmentItem = db.EquipmentItems.Find(id);
            if (equipmentItem == null)
            {
                return HttpNotFound();
            }
            return View(equipmentItem);
        }

        // GET: EquipmentItems/Create
        public ActionResult Create()
        {
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name");
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name");
            return View();
        }

        // POST: EquipmentItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EquipmentId,ItemId,QuantityRequired,QuantityRequiredSpare,UnitOfMeasure,Notes,ValidFrom,ValidTo")] EquipmentItem equipmentItem)
        {
            if (ModelState.IsValid)
            {
                db.EquipmentItems.Add(equipmentItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", equipmentItem.EquipmentId);
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", equipmentItem.ItemId);
            return View(equipmentItem);
        }

        // GET: EquipmentItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentItem equipmentItem = db.EquipmentItems.Find(id);
            if (equipmentItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", equipmentItem.EquipmentId);
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", equipmentItem.ItemId);
            return View(equipmentItem);
        }

        // POST: EquipmentItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EquipmentId,ItemId,QuantityRequired,QuantityRequiredSpare,UnitOfMeasure,Notes,ValidFrom,ValidTo")] EquipmentItem equipmentItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipmentItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", equipmentItem.EquipmentId);
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", equipmentItem.ItemId);
            return View(equipmentItem);
        }

        // GET: EquipmentItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentItem equipmentItem = db.EquipmentItems.Find(id);
            if (equipmentItem == null)
            {
                return HttpNotFound();
            }
            return View(equipmentItem);
        }

        // POST: EquipmentItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipmentItem equipmentItem = db.EquipmentItems.Find(id);
            db.EquipmentItems.Remove(equipmentItem);
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
