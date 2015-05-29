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
    public class EquipmentModulesController : Controller
    {
        private EquipmentManagerEntities db = new EquipmentManagerEntities();

        // GET: EquipmentModules
        public ActionResult Index()
        {
            var equipmentModules = db.EquipmentModules.Include(e => e.Equipment).Include(e => e.ParentModule);
            return View(equipmentModules.ToList());
        }

        // GET: EquipmentModules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentModule equipmentModule = db.EquipmentModules.Find(id);
            if (equipmentModule == null)
            {
                return HttpNotFound();
            }
            return View(equipmentModule);
        }

        // GET: EquipmentModules/Create
        public ActionResult Create()
        {
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name");
            ViewBag.ParentModuleId = new SelectList(db.EquipmentModules, "Id", "Name");
            return View();
        }

        // POST: EquipmentModules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,EquipmentId,ParentModuleId")] EquipmentModule equipmentModule)
        {
            if (ModelState.IsValid)
            {
                db.EquipmentModules.Add(equipmentModule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", equipmentModule.EquipmentId);
            ViewBag.ParentModuleId = new SelectList(db.EquipmentModules, "Id", "Name", equipmentModule.ParentModuleId);
            return View(equipmentModule);
        }

        // GET: EquipmentModules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentModule equipmentModule = db.EquipmentModules.Find(id);
            if (equipmentModule == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", equipmentModule.EquipmentId);
            ViewBag.ParentModuleId = new SelectList(db.EquipmentModules, "Id", "Name", equipmentModule.ParentModuleId);
            return View(equipmentModule);
        }

        // POST: EquipmentModules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,EquipmentId,ParentModuleId")] EquipmentModule equipmentModule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipmentModule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", equipmentModule.EquipmentId);
            ViewBag.ParentModuleId = new SelectList(db.EquipmentModules, "Id", "Name", equipmentModule.ParentModuleId);
            return View(equipmentModule);
        }

        // GET: EquipmentModules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentModule equipmentModule = db.EquipmentModules.Find(id);
            if (equipmentModule == null)
            {
                return HttpNotFound();
            }
            return View(equipmentModule);
        }

        // POST: EquipmentModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipmentModule equipmentModule = db.EquipmentModules.Find(id);
            db.EquipmentModules.Remove(equipmentModule);
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
