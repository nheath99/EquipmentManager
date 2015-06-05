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
    public class InstallationEquipmentModulesController : Controller
    {
        private EquipmentManagerEntities db = new EquipmentManagerEntities();
        
        // GET: InstallationEquipmentModules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallationEquipmentModule installationEquipmentModule = db.InstallationEquipmentModules.Find(id);
            if (installationEquipmentModule == null)
            {
                return HttpNotFound();
            }
            return View(installationEquipmentModule);
        }
        
        // GET: InstallationEquipmentModules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallationEquipmentModule installationEquipmentModule = db.InstallationEquipmentModules.Find(id);
            if (installationEquipmentModule == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstallationId = new SelectList(db.Installations, "Id", "Name", installationEquipmentModule.InstallationId);
            ViewBag.ParentModuleId = new SelectList(db.InstallationEquipmentModules, "Id", "Id", installationEquipmentModule.ParentModuleId);
            ViewBag.EquipmentModuleId = new SelectList(db.EquipmentModules, "Id", "Name", installationEquipmentModule.EquipmentModuleId);
            return View(installationEquipmentModule);
        }

        // POST: InstallationEquipmentModules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EquipmentModuleId,InstallationId,ActualCost,ParentModuleId")] InstallationEquipmentModule installationEquipmentModule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(installationEquipmentModule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstallationId = new SelectList(db.Installations, "Id", "Name", installationEquipmentModule.InstallationId);
            ViewBag.ParentModuleId = new SelectList(db.InstallationEquipmentModules, "Id", "Id", installationEquipmentModule.ParentModuleId);
            ViewBag.EquipmentModuleId = new SelectList(db.EquipmentModules, "Id", "Name", installationEquipmentModule.EquipmentModuleId);
            return View(installationEquipmentModule);
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
