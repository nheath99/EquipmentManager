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
    public class PartsController : Controller
    {
        private EquipmentManagerEntities db = new EquipmentManagerEntities();

        // GET: Parts
        public ActionResult Index()
        {
            var Parts = db.Parts.Include(i => i.PartCategory).Include(i => i.Replaces).Include(i => i.Manufacturer).Include(i => i.Supplier);
            return View(Parts.ToList());
        }

        public JsonResult AddPartNumber(int partId, string value, string description)
        {
            var Part = db.Parts.Find(partId);
            if (Part != null)
            {
                try
                {
                    PartNumber pn = new PartNumber()
                    {
                        Value = value,
                        Description = description,
                        Active = true
                    };
                    Part.PartNumbers.Add(pn);
                    db.SaveChanges();
                    return Json(new { result = true, id = pn.Id, value = pn.Value, description = pn.Description }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {

                }
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePartNumber(int id)
        {
            var pn = db.PartNumbers.Find(id);
            if (pn != null)
            {
                try
                {
                    db.PartNumbers.Remove(pn);
                    db.SaveChanges();
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                }
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: Parts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part Part = db.Parts.Find(id);
            if (Part == null)
            {
                return HttpNotFound();
            }
            Membership.AddRecentPartToUser(User.Identity.GetUserName(), Part.Id);
            return View(Part);
        }

        public JsonResult Search(string q)
        {
            var Parts = db.Parts.Where(x => !x.Obsolete).ToList();

            var retParts = Parts
                .Where(x => x.SearchString.ToLower().Contains(q.ToLower()))
                .Select(x => new
                {
                    value = x.Id,
                    label = x.DisplayString,
                    name = x.Name,
                    manufacturer = x.ManufacturerName,
                    supplier = x.SupplierName,
                    itemsPerUnit = x.ItemsPerUnit,
                    link = x.Link,
                    partNumbers = x.PartNumbersList
                });
            return Json(retParts, JsonRequestBehavior.AllowGet);
        }

        // GET: Parts/Create
        public ActionResult Create()
        {
            var vm = new CreatePartViewModel()
            {
                ItemsPerUnit = 1
            };

            ViewBag.CategoryId = new SelectList(db.PartCategories.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers.OrderBy(x => x.Name), "Id", "Name");
            return View(vm);
        }

        // POST: Parts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,SupplierId,ManufacturerId,ItemsPerUnit,CategoryId,Link,PartNumberValues,PartNumberDescriptions,Continue")] CreatePartViewModel partVm)
        {
            if (ModelState.IsValid)
            {
                db.Parts.Add(partVm.GetPart());
                db.SaveChanges();
                
                if (partVm.Continue == "Create and Continue")
                {
                    return RedirectToAction("Create");
                }                
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.PartCategories.OrderBy(x => x.Name), "Id", "Name", partVm.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.OrderBy(x => x.Name), "Id", "Name", partVm.ManufacturerId);
            ViewBag.SupplierId = new SelectList(db.Suppliers.OrderBy(x => x.Name), "Id", "Name", partVm.SupplierId);
            return View(partVm);
        }

        // GET: Parts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part Part = db.Parts.Find(id);
            if (Part == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.PartCategories.OrderBy(x => x.Name), "Id", "Name", Part.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.OrderBy(x => x.Name), "Id", "Name", Part.ManufacturerId);
            ViewBag.SupplierId = new SelectList(db.Suppliers.OrderBy(x => x.Name), "Id", "Name", Part.SupplierId);
            return View(Part);
        }

        // POST: Parts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,SupplierId,ManufacturerId,PartsPerUnit,CategoryId,Link,Obsolete,PartNumberValues,PartNumberDescriptions")] Part Part)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Part).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.PartCategories.OrderBy(x => x.Name), "Id", "Name", Part.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.OrderBy(x => x.Name), "Id", "Name", Part.ManufacturerId);
            ViewBag.SupplierId = new SelectList(db.Suppliers.OrderBy(x => x.Name), "Id", "Name", Part.SupplierId);
            return View(Part);
        }

        // GET: Parts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part Part = db.Parts.Find(id);
            if (Part == null)
            {
                return HttpNotFound();
            }
            return View(Part);
        }

        // POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Part Part = db.Parts.Find(id);
            Membership.GetOrCreateUser(User.Identity.GetUserName()).RemovePart(id);
            db.Parts.Remove(Part);
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
