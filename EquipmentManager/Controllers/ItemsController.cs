using EquipmentManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquipmentManager.Controllers
{
    public class ItemsController : Controller
    {
        EquipmentManagerEntities db = new EquipmentManagerEntities();

        public ActionResult Index()
        {
            var items = db.Items.ToList();
            return View(items);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var item = db.Items.Find(id.Value);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            ViewBag.Manufacturers = new SelectList(db.Manufacturers, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.ItemCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Name,Description,SupplierId,ManufacturerId,ItemsPerUnit,CategoryId,Link,Obsolete,ReplacedBy_Id")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", item.SupplierId);
            ViewBag.Manufacturers = new SelectList(db.Manufacturers, "Id", "Name", item.ManufacturerId);
            ViewBag.CategoryId = new SelectList(db.ItemCategories, "Id", "Name");
            return View(item);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var item = db.Items.Find(id.Value);
            if (item == null)
            {
                return HttpNotFound();
            }

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", item.SupplierId);
            ViewBag.Manufacturers = new SelectList(db.Manufacturers, "Id", "Name", item.ManufacturerId);
            ViewBag.CategoryId = new SelectList(db.ItemCategories, "Id", "Name");
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,SupplierId,ManufacturerId,ItemsPerUnit,CategoryId,Link,Obsolete,ReplacedBy_Id")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", item.SupplierId);
            ViewBag.Manufacturers = new SelectList(db.Manufacturers, "Id", "Name", item.ManufacturerId);
            ViewBag.CategoryId = new SelectList(db.ItemCategories, "Id", "Name");
            return View(item);
        }
    }
}