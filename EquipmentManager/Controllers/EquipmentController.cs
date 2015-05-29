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

            ViewBag.UnitsOfMeasure = new SelectList(db.UnitsOfMeasures.ToList(), "Id", "Name");
            ViewBag.Sites = new SelectList(db.Sites.OrderBy(x => x.Code).ToList(), "Id", "CodeName");
            ViewBag.Suppliers = new SelectList(db.Suppliers.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.TemporalUnit = EnumHelpers.AsSelectList<TemporalUnit>();
            Membership.AddRecentEquipmentToUser(User.Identity.GetUserName(), equipment.Id);
            return View(new EquipmentViewModel(equipment));
        }

        public JsonResult AddModule(int equipmentId, string name, string description, int? parentModuleId)
        {
            try
            {
                Equipment e = db.Equipments.Find(equipmentId);
                if (e != null)
                {
                    EquipmentModule em = new EquipmentModule()
                    {
                        Name = name,
                        Description = description,
                        ParentModuleId = parentModuleId
                    };
                    e.EquipmentModules.Add(em);
                    db.SaveChanges();
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveModule(int moduleId)
        {
            try
            {
                EquipmentModule em = db.EquipmentModules.Find(moduleId);
                if (em != null)
                {
                    db.EquipmentModules.Remove(em);
                    db.SaveChanges();
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }                
            }
            catch (Exception)
            {
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddLabour(int equipmentId, int moduleId, string name, string description, Nullable<int> supplierId, float quantity, int quantityUnit)
        {
            Equipment e = db.Equipments.Find(equipmentId);
            if (e != null)
            {
                EquipmentModule em = e.EquipmentModules.SingleOrDefault(x => x.Id == moduleId);
                if (em != null)
                {
                    EquipmentLabour el = new EquipmentLabour()
                    {
                        Name = name,
                        Description = description,
                        SupplierId = supplierId,
                        Quantity = quantity,
                        QuantityUnit = (TemporalUnit)quantityUnit
                    };

                    em.EquipmentLabours.Add(el);
                    db.SaveChanges();

                    el = db.EquipmentLabours.Include(x => x.Supplier).Single(x => x.Id == el.Id);
                    return Json(new { result = true, id = el.Id, name = el.Name, description = el.Description, supplierId = el.SupplierId, supplierName = el.SupplierId != null ? el.Supplier.Name : string.Empty, quantity = Math.Round(el.Quantity, 2).ToString() + " " + el.QuantityUnit.Name() }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveLabour(int labourId)
        {
            EquipmentLabour el = db.EquipmentLabours.Find(labourId);
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
                }
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddEquipmentPart(int equipmentId, int moduleId, int partId, double quantityRequired, double? quantitySpare, string unitOfMeasure, string notes)
        {
            try
            {
                Equipment e = db.Equipments.Find(equipmentId);
                if (e != null)
                {
                    EquipmentModule em = db.EquipmentModules.SingleOrDefault(x => x.Id == moduleId);
                    if (em != null)
                    {
                        EquipmentPart ep = new EquipmentPart()
                        {
                            EquipmentModuleId = moduleId,
                            PartId = partId,
                            QuantityRequired = quantityRequired,
                            QuantityRequiredSpare = quantitySpare ?? 0,
                            UnitOfMeasure = unitOfMeasure,
                            Notes = notes,
                            ValidFrom = DateTime.Today
                        };

                        em.EquipmentParts.Add(ep);
                        db.SaveChanges();

                        ep = db.EquipmentParts.Include(x => x.Part).Single(x => x.Id == ep.Id);

                        return Json(new
                        {
                            id = ep.Id,
                            itemId = ep.PartId,
                            name = ep.Part.Name,
                            manufacturer = ep.Part.ManufacturerName,
                            supplier = ep.Part.SupplierName,
                            quantityRequired = ep.QuantityRequired,
                            quantitySpare = ep.QuantityRequiredSpare,
                            quantityString = ep.QuantityString,
                            unitOfMeasure = ep.UnitOfMeasure,
                            notes = ep.Notes
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveEquipmentPart(int equipmentPartId)
        {
            EquipmentPart e = db.EquipmentParts.Find(equipmentPartId);
            if (e != null)
            {
                try
                {
                    db.EquipmentParts.Remove(e);
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

        public JsonResult MoveEquipmentPart(int equipmentPartId)
        {
            try
            {
                var ep = db.EquipmentParts.Find(equipmentPartId);
                if (ep != null)
                {
                    if (ep.EquipmentModuleId != null && ep.EquipmentModule.ParentModuleId != null)
                    {
                        ep.EquipmentModuleId = ep.EquipmentModule.ParentModuleId.Value;
                        db.SaveChanges();
                        return Json(new { result = true, newModuleId = ep.EquipmentModuleId }, JsonRequestBehavior.AllowGet);
                    }                    
                }
            }
            catch (Exception)
            {
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult AddEquipmentPartTag(int id, string tagName)
        //{
        //    try
        //    {
        //        var ei = db.EquipmentParts.Find(id);
        //        if (ei != null)
        //        {
        //            var tag = db.Tags.SingleOrDefault(x => x.Name == tagName.ToLower());
        //            if (tag == null)
        //            {
        //                tag = new Tag()
        //                {
        //                    Name = tagName.ToLower()
        //                };
        //            }
        //            ei.Tags.Add(tag);
        //            db.SaveChanges();
        //            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult AddEquipmentLabourTag(int id, string tagName)
        //{
        //    try
        //    {
        //        var el = db.EquipmentLabours.Find(id);
        //        if (el != null)
        //        {
        //            var tag = db.Tags.SingleOrDefault(x => x.Name == tagName.ToLower());
        //            if (tag == null)
        //            {
        //                tag = new Tag()
        //                {
        //                    Name = tagName.ToLower()
        //                };
        //                el.Tags.Add(tag);
        //                db.SaveChanges();
        //                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult SearchTags(string q)
        {
            var tags = db.Tags.Where(x => x.Name.ToLower().Contains(q.ToLower())).ToList();
            return Json(new { tags = tags.Select(x => new { tagName = x.Name}) }, JsonRequestBehavior.AllowGet);
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
