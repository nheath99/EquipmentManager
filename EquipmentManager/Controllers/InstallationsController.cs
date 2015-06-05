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
    public class InstallationsController : Controller
    {
        private EquipmentManagerEntities db = new EquipmentManagerEntities();

        // GET: Installations
        public ActionResult Index()
        {
            var installations = db.Installations.Include(i => i.Equipment).Include(i => i.Site);
            return View(installations.ToList());
        }

        public JsonResult AddInstallation(int equipmentId, int siteId, string name, string description, bool importModules)
        {
            try
            {
                Equipment e = db.Equipments.Find(equipmentId);
                if (e != null)
                {
                    Installation i = new Installation()
                    {
                        EquipmentId = equipmentId,
                        Destination_SiteId = siteId,
                        Name = name,
                        Description = description
                    };
                    db.Installations.Add(i);

                    if (importModules)
                    {
                        foreach (var module in e.TopLevelModules)
                        {
                            i.InstallationEquipmentModules.Add(module.InstallationModule(i, null));
                        }
                    }

                    db.SaveChanges();
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: Installations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Installation installation = db.Installations.Find(id);
            if (installation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Statuses = EnumHelpers.AsSelectList<InstallationPartStatus>();
            ViewBag.LabourStatuses = EnumHelpers.AsSelectList<LabourStatus>();
            ViewBag.Units = EnumHelpers.AsSelectList<TemporalUnit>();
            ViewBag.HasMissingParts = installation.MissingParts.Count() != 0;
            Membership.AddRecentInstallationToUser(User.Identity.GetUserName(), installation.Id);
            return View(installation);
        }

        public JsonResult PartDetails(int installationEquipmentPatId)
        {
            try
            {
                var p = db.InstallationEquipmentParts.Find(installationEquipmentPatId);
                if (p != null)
                {
                    return Json(
                        new
                        { 
                            result = true,
                            partId = p.Id,
                            name = p.Name,
                            description = p.EquipmentPart.Part.Description,
                            manufacturer = p.EquipmentPart.Part.ManufacturerName,
                            supplier = p.EquipmentPart.Part.SupplierName,
                            itemsPerUnit = p.EquipmentPart.Part.ItemsPerUnit,
                            quantityRequired = p.EquipmentPart.QuantityRequired,
                            statusId = (int)p.StatusId,
                            costPerUnit = p.CostPerUnit,
                            unitsOrdered = p.UnitsOrdered,
                            postage = p.Postage,
                            actualCost = p.ActualCost
                        }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePart(int id, int statusId, decimal? costPerUnit, double? unitsOrdered, decimal? postage, decimal? actualCost)
        {
            try
            {
                var p = db.InstallationEquipmentParts.Find(id);
                if (p != null)
                {
                    p.StatusId = (InstallationPartStatus)statusId;
                    p.CostPerUnit = costPerUnit;
                    p.UnitsOrdered = unitsOrdered;
                    p.Postage = postage;
                    p.ActualCost = actualCost;

                    db.SaveChanges();
                    return Json(new
                    { 
                        result = true,
                        statusId = (int)p.StatusId,
                        statusName = p.StatusId.Name(),
                        unitsOrdered = p.UnitsOrdered,
                        costPerUnit = p.CostPerUnit,
                        postage = p.Postage,
                        quotedCost = p.QuotedCost,
                        cost = p.Cost
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Parts(int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Installation installation = db.Installations.Find(id);
            if (installation == null)
            {
                return HttpNotFound();
            }
            return View(installation);
        }

        public ActionResult Labour(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Installation installation = db.Installations.Find(id);
            if (installation == null)
            {
                return HttpNotFound();
            }
            return View(installation);
        }

        public ActionResult Modules(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Installation installation = db.Installations.Find(id);
            if (installation == null)
            {
                return HttpNotFound();
            }
            return View(installation);
        }

        public ActionResult MissingParts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Installation installation = db.Installations.Find(id);
            if (installation == null)
            {
                return HttpNotFound();
            }

            return View(installation);
        }

        public JsonResult AddMissingEquipmentParts(int installationId)
        {
            try
            {
                Installation i = db.Installations.Find(installationId);
                foreach (var item in i.MissingParts)
                {
                    AddEquipmentPart(installationId, item.Id);
                }
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddEquipmentPart(int installationId, int equipmentPartId)
        {
            try
            {
                Installation i = db.Installations.Find(installationId);
                EquipmentPart p = db.EquipmentParts.Find(equipmentPartId);
                InstallationEquipmentModule iem = i.InstallationEquipmentModules.SingleOrDefault(x => x.EquipmentModuleId == p.EquipmentModuleId);
                if (i != null && p != null && iem != null && i.EquipmentId == p.EquipmentModule.EquipmentId)
                {
                    InstallationEquipmentPart iep = new InstallationEquipmentPart()
                    {
                        InstallationEquipmentModule = iem,
                        EquipmentPart = p
                    };

                    db.InstallationEquipmentParts.Add(iep);
                    db.SaveChanges();

                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveInstallationEquipmentPart(int id)
        {
            try
            {
                InstallationEquipmentPart i = db.InstallationEquipmentParts.Find(id);
                if (i != null)
                {
                    db.InstallationEquipmentParts.Remove(i);
                    db.SaveChanges();

                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: Installations/Create
        public ActionResult Create()
        {
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name");
            ViewBag.Destination_SiteId = new SelectList(db.Sites, "Id", "Name");
            return View();
        }

        // POST: Installations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,EquipmentId,Destination_SiteId")] Installation installation)
        {
            if (ModelState.IsValid)
            {
                db.Installations.Add(installation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", installation.EquipmentId);
            ViewBag.Destination_SiteId = new SelectList(db.Sites, "Id", "Name", installation.Destination_SiteId);
            return View(installation);
        }

        // GET: Installations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Installation installation = db.Installations.Find(id);
            if (installation == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", installation.EquipmentId);
            ViewBag.Destination_SiteId = new SelectList(db.Sites, "Id", "Name", installation.Destination_SiteId);
            return View(installation);
        }

        // POST: Installations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,EquipmentId,Destination_SiteId")] Installation installation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(installation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "Name", installation.EquipmentId);
            ViewBag.Destination_SiteId = new SelectList(db.Sites, "Id", "Name", installation.Destination_SiteId);
            return View(installation);
        }

        // GET: Installations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Installation installation = db.Installations.Find(id);
            if (installation == null)
            {
                return HttpNotFound();
            }
            return View(installation);
        }

        // POST: Installations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Installation installation = db.Installations.Find(id);
            db.Installations.Remove(installation);
            Membership.GetOrCreateUser(User.Identity.GetUserName()).RemoveInstallation(id);
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
