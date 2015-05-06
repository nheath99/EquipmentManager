using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EquipmentManager.Data;
using EquipmentManager.Models;

namespace EquipmentManager.Controllers
{
    public class EquipmentController : Controller
    {
        private EquipmentManagerEntities db = new EquipmentManagerEntities();

        // GET: Equipment
        public ActionResult Index()
        {
            return View(db.Equipments.ToList());
        }

        // GET: Equipment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnitsOfMeasure = new SelectList(db.UnitsOfMeasures.ToList(), "Name", "Name");
            return View(new EquipmentViewModel(equipment));
        }

        public JsonResult RemoveEquipmentItem(int id)
        {
            EquipmentItem e = db.EquipmentItems.Find(id);
            if (e != null)
            {
                db.EquipmentItems.Remove(e);
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddEquipmentItem(int equipmentId, int itemId, double quantityRequired, double? quantitySpare, string unitOfMeasure, string notes)
        {
            EquipmentItem eq = new EquipmentItem()
            {
                EquipmentId = equipmentId,
                ItemId = itemId,
                QuantityRequired = quantityRequired,
                QuantityRequiredSpare = quantitySpare ?? 0,
                UnitOfMeasure = unitOfMeasure,
                Notes = notes
            };

            db.EquipmentItems.Add(eq);
            db.SaveChanges();

            eq = db.EquipmentItems.Include(x => x.Item).Single(x => x.Id == eq.Id);

            return Json(new
            {
                id = eq.Id,
                itemId = eq.ItemId,
                name = eq.Item.Name,
                manufacturer = eq.Item.Manufacturer.Name,
                supplier = eq.Item.Supplier.Name,
                quantityRequired = eq.QuantityRequired,
                quantitySpare = eq.QuantityRequiredSpare,
                unitOfMeasure = eq.UnitOfMeasure,
                notes = eq.Notes
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: Equipment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Equipments.Add(equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipment);
        }

        // GET: Equipment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Equipment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipment);
        }

        // GET: Equipment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Equipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipment equipment = db.Equipments.Find(id);
            db.Equipments.Remove(equipment);
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
