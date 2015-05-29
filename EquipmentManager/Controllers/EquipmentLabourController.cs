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
    public class EquipmentLabourController : Controller
    {
        private EquipmentManagerEntities db = new EquipmentManagerEntities();

        // GET: EquipmentLabours
        public ActionResult Index()
        {
            var equipmentLabours = db.EquipmentLabours.Include(e => e.EquipmentModule).Include(e => e.Supplier);
            return View(equipmentLabours.ToList());
        }

        // GET: EquipmentLabours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentLabour equipmentLabour = db.EquipmentLabours.Find(id);
            if (equipmentLabour == null)
            {
                return HttpNotFound();
            }
            return View(equipmentLabour);
        }

        // GET: EquipmentLabours/Create
        public ActionResult Create()
        {
            ViewBag.EquipmentModuleId = new SelectList(db.EquipmentModules, "Id", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            return View();
        }

        // POST: EquipmentLabours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EquipmentModuleId,Name,Description,SupplierId,Quantity,QuantityUnit")] EquipmentLabour equipmentLabour)
        {
            if (ModelState.IsValid)
            {
                db.EquipmentLabours.Add(equipmentLabour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EquipmentModuleId = new SelectList(db.EquipmentModules, "Id", "Name", equipmentLabour.EquipmentModuleId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", equipmentLabour.SupplierId);
            return View(equipmentLabour);
        }

        // GET: EquipmentLabours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentLabour equipmentLabour = db.EquipmentLabours.Find(id);
            if (equipmentLabour == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentModuleId = new SelectList(db.EquipmentModules, "Id", "Name", equipmentLabour.EquipmentModuleId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", equipmentLabour.SupplierId);
            return View(equipmentLabour);
        }

        // POST: EquipmentLabours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EquipmentModuleId,Name,Description,SupplierId,Quantity,QuantityUnit")] EquipmentLabour equipmentLabour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipmentLabour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentModuleId = new SelectList(db.EquipmentModules, "Id", "Name", equipmentLabour.EquipmentModuleId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", equipmentLabour.SupplierId);
            return View(equipmentLabour);
        }

        // GET: EquipmentLabours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentLabour equipmentLabour = db.EquipmentLabours.Find(id);
            if (equipmentLabour == null)
            {
                return HttpNotFound();
            }
            return View(equipmentLabour);
        }

        // POST: EquipmentLabours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipmentLabour equipmentLabour = db.EquipmentLabours.Find(id);
            db.EquipmentLabours.Remove(equipmentLabour);
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
