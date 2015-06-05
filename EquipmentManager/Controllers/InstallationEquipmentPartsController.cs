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
    public class InstallationEquipmentPartsController : Controller
    {
        private EquipmentManagerEntities db = new EquipmentManagerEntities();
        
        // GET: InstallationEquipmentParts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallationEquipmentPart installationEquipmentPart = db.InstallationEquipmentParts.Find(id);
            if (installationEquipmentPart == null)
            {
                return HttpNotFound();
            }
            return View(installationEquipmentPart);
        }

        // GET: InstallationEquipmentParts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallationEquipmentPart installationEquipmentPart = db.InstallationEquipmentParts.Find(id);
            if (installationEquipmentPart == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstallationModuleId = new SelectList(db.InstallationEquipmentModules, "Id", "Id", installationEquipmentPart.InstallationModuleId);
            ViewBag.EquipmentPartId = new SelectList(db.EquipmentParts, "Id", "UnitOfMeasure", installationEquipmentPart.EquipmentPartId);
            return View(installationEquipmentPart);
        }

        // POST: InstallationEquipmentParts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InstallationModuleId,EquipmentPartId,DateQuoted,DateOrdered,DateExpected,DateReceived,StatusId,CostPerUnit,UnitsOrdered,Postage,ActualCost")] InstallationEquipmentPart installationEquipmentPart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(installationEquipmentPart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstallationModuleId = new SelectList(db.InstallationEquipmentModules, "Id", "Id", installationEquipmentPart.InstallationModuleId);
            ViewBag.EquipmentPartId = new SelectList(db.EquipmentParts, "Id", "UnitOfMeasure", installationEquipmentPart.EquipmentPartId);
            return View(installationEquipmentPart);
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
