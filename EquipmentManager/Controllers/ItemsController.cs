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
    public class ItemsController : Controller
    {
        private EquipmentManagerEntities db = new EquipmentManagerEntities();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.ItemCategory).Include(i => i.Item1).Include(i => i.Manufacturer).Include(i => i.Supplier);
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

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.ItemCategories, "Id", "Name");
            ViewBag.ReplacedBy_Id = new SelectList(db.Items, "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "Id", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,SupplierId,ManufacturerId,ItemsPerUnit,CategoryId,Link,Obsolete,ReplacedBy_Id")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.ItemCategories, "Id", "Name", item.CategoryId);
            ViewBag.ReplacedBy_Id = new SelectList(db.Items, "Id", "Name", item.ReplacedBy_Id);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "Id", "Name", item.ManufacturerId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", item.SupplierId);
            return View(item);
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
            ViewBag.CategoryId = new SelectList(db.ItemCategories, "Id", "Name", item.CategoryId);
            ViewBag.ReplacedBy_Id = new SelectList(db.Items, "Id", "Name", item.ReplacedBy_Id);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "Id", "Name", item.ManufacturerId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", item.SupplierId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,SupplierId,ManufacturerId,ItemsPerUnit,CategoryId,Link,Obsolete,ReplacedBy_Id")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.ItemCategories, "Id", "Name", item.CategoryId);
            ViewBag.ReplacedBy_Id = new SelectList(db.Items, "Id", "Name", item.ReplacedBy_Id);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "Id", "Name", item.ManufacturerId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", item.SupplierId);
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
