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
    public class ItemsController : Controller
    {
        private EquipmentManagerEntities db = new EquipmentManagerEntities();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.ItemCategory).Include(i => i.Replaces).Include(i => i.Manufacturer).Include(i => i.Supplier);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public JsonResult Search(string q)
        {
            var items = db.Items.Where(x => !x.Obsolete).ToList();

            var retItems = items
                .Where(x => x.SearchString.ToLower().Contains(q.ToLower()))
                .Select(x => new { value = x.Id, label = x.DisplayString });
            return Json(retItems, JsonRequestBehavior.AllowGet);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.ItemCategories.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers.OrderBy(x => x.Name), "Id", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,SupplierId,ManufacturerId,ItemsPerUnit,CategoryId,Link,PartNumberValues,PartNumberDescriptions")] CreateItemViewModel itemVm)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(itemVm.GetItem());
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.ItemCategories.OrderBy(x => x.Name), "Id", "Name", itemVm.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.OrderBy(x => x.Name), "Id", "Name", itemVm.ManufacturerId);
            ViewBag.SupplierId = new SelectList(db.Suppliers.OrderBy(x => x.Name), "Id", "Name", itemVm.SupplierId);
            return View(itemVm);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.ItemCategories.OrderBy(x => x.Name), "Id", "Name", item.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.OrderBy(x => x.Name), "Id", "Name", item.ManufacturerId);
            ViewBag.SupplierId = new SelectList(db.Suppliers.OrderBy(x => x.Name), "Id", "Name", item.SupplierId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,SupplierId,ManufacturerId,ItemsPerUnit,CategoryId,Link,Obsolete,PartNumberValues,PartNumberDescriptions")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.ItemCategories.OrderBy(x => x.Name), "Id", "Name", item.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.OrderBy(x => x.Name), "Id", "Name", item.ManufacturerId);
            ViewBag.SupplierId = new SelectList(db.Suppliers.OrderBy(x => x.Name), "Id", "Name", item.SupplierId);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
