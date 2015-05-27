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
    public class ItemCategoriesController : Controller
    {
        private EquipmentManagerEntities db = new EquipmentManagerEntities();

        // GET: ItemCategories
        public ActionResult Index()
        {
            return View(db.PartCategories.ToList());
        }

        // GET: ItemCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartCategory partCategory = db.PartCategories.Find(id);
            if (partCategory == null)
            {
                return HttpNotFound();
            }
            return View(partCategory);
        }

        // GET: ItemCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] PartCategory partCategory)
        {
            if (ModelState.IsValid)
            {
                db.PartCategories.Add(partCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partCategory);
        }

        // GET: ItemCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartCategory partCategory = db.PartCategories.Find(id);
            if (partCategory == null)
            {
                return HttpNotFound();
            }
            return View(partCategory);
        }

        // POST: ItemCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] PartCategory partCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partCategory);
        }

        // GET: ItemCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartCategory partCategory = db.PartCategories.Find(id);
            if (partCategory == null)
            {
                return HttpNotFound();
            }
            return View(partCategory);
        }

        // POST: ItemCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PartCategory partCategory = db.PartCategories.Find(id);
            db.PartCategories.Remove(partCategory);
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
