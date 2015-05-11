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
            ViewBag.Sites = new SelectList(db.Sites.OrderBy(x => x.Code).ToList(), "Id", "CodeName");
            ViewBag.Suppliers = new SelectList(db.Suppliers.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.TemporalUnit = ExtensionMethods.AsSelectList<TemporalUnit>();
            return View(new EquipmentViewModel(equipment));
        }

        public JsonResult AddLabour(int equipmentId, string name, string description, Nullable<int> supplierId, float quantity, int quantityUnit)
        {
            Equipment eq = db.Equipments.Find(equipmentId);
            if (eq != null)
            {
                EquipmentLabour el = new EquipmentLabour()
                {
                    Equipment = eq,
                    Name = name,
                    Description = description,
                    SupplierId = supplierId,
                    Quantity = quantity,
                    QuantityUnit = (TemporalUnit)quantityUnit
                };

                db.EquipmentLabours.Add(el);
                db.SaveChanges();

                el = db.EquipmentLabours.Include(x => x.Supplier).Single(x => x.Id == el.Id);
                return Json(new { result = true, id = el.Id, name = el.Name, description = el.Description, supplierId = el.SupplierId, supplierName = el.SupplierId != null ? el.Supplier.Name : string.Empty, quantity = Math.Round(el.Quantity, 2).ToString() + " " + el.QuantityUnit.Name() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveLabour(int id)
        {
            EquipmentLabour el = db.EquipmentLabours.Find(id);
            if (el != null)
            {
                try
                {
                    db.EquipmentLabours.Remove(el);
                    db.SaveChanges();
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { result = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RemoveEquipmentItem(int id)
        {
            EquipmentItem e = db.EquipmentItems.Find(id);
            if (e != null)
            {
                try
                {
                    db.EquipmentItems.Remove(e);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    e.ValidTo = DateTime.Today;
                    db.SaveChanges();
                }
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
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
                Notes = notes,
                ValidFrom = DateTime.Today
            };

            db.EquipmentItems.Add(eq);
            db.SaveChanges();

            eq = db.EquipmentItems.Include(x => x.Item).Single(x => x.Id == eq.Id);

            return Json(new
            {
                id = eq.Id,
                itemId = eq.ItemId,
                name = eq.Item.Name,
                manufacturer = eq.Item.ManufacturerName,
                supplier = eq.Item.SupplierName,
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
