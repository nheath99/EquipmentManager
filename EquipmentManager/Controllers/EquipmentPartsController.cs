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

        // GET: EquipmentParts
        public ActionResult Index()
        {
            var equipmentParts = db.EquipmentParts.Include(e => e.EquipmentModule).Include(e => e.Part);
            return View(equipmentParts.ToList());
        }

        // GET: EquipmentParts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentPart equipmentPart = db.EquipmentParts.Find(id);
            if (equipmentPart == null)
            {
                return HttpNotFound();
            }
            return View(equipmentPart);
        }

        // GET: EquipmentParts/Create
        public ActionResult Create()
        {
            ViewBag.EquipmentModuleId = new SelectList(db.EquipmentModules, "Id", "Name");
            ViewBag.PartId = new SelectList(db.Parts, "Id", "Name");
            return View();
        }

        // POST: EquipmentParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EquipmentModuleId,PartId,QuantityRequired,QuantityRequiredSpare,UnitOfMeasure,Notes,ValidFrom,ValidTo")] EquipmentPart equipmentPart)
        {
            if (ModelState.IsValid)
            {
                db.EquipmentParts.Add(equipmentPart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EquipmentModuleId = new SelectList(db.EquipmentModules, "Id", "Name", equipmentPart.EquipmentModuleId);
            ViewBag.PartId = new SelectList(db.Parts, "Id", "Name", equipmentPart.PartId);
            return View(equipmentPart);
        }

        // GET: EquipmentParts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentPart equipmentPart = db.EquipmentParts.Find(id);
            if (equipmentPart == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentModuleId = equipmentPart.EquipmentModule.Equipment.ModuleList(equipmentPart.EquipmentModuleId);
            return View(equipmentPart);
        }

        // POST: EquipmentParts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EquipmentModuleId,PartId,QuantityRequired,QuantityRequiredSpare,UnitOfMeasure,Notes,ValidFrom,ValidTo")] EquipmentPart equipmentPart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipmentPart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentModuleId = equipmentPart.EquipmentModule.Equipment.ModuleList(equipmentPart.EquipmentModuleId);
            return View(equipmentPart);
        }

        // GET: EquipmentParts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentPart equipmentPart = db.EquipmentParts.Find(id);
            if (equipmentPart == null)
            {
                return HttpNotFound();
            }
            return View(equipmentPart);
        }

        // POST: EquipmentParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipmentPart equipmentPart = db.EquipmentParts.Find(id);
            db.EquipmentParts.Remove(equipmentPart);
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
