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

        public JsonResult AddInstallation(int equipmentId, int siteId, string name, string description)
        {
            Installation i = new Installation()
            {
                EquipmentId = equipmentId,
                Destination_SiteId = siteId,
                Name = name,
                Description = description
            };
            db.Installations.Add(i);
            db.SaveChanges();
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddInstallationEquipmentItem(int installationId, int equipmentItemId)
        {
            try
            {
                Installation i = db.Installations.Find(installationId);
                EquipmentItem ei = db.EquipmentItems.Find(equipmentItemId);

                if (i != null && ei != null && i.EquipmentId == ei.EquipmentId)
                {
                    InstallationEquipmentItem iei = new InstallationEquipmentItem()
                    {
                        Installation = i,
                        EquipmentItem = ei,
                        StatusId = ItemStatus.None
                    };
                    i.InstallationEquipmentItems.Add(iei);
                    db.SaveChanges();
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateInstallationEquipmentItem(int id, double? qtyOrdered, int statusId, decimal? costPerUnit, decimal? postage, decimal? actualCost)
        {
            try
            {
                InstallationEquipmentItem i = db.InstallationEquipmentItems.Find(id);
                if (i != null)
                {
                    i.UnitsOrdered = qtyOrdered;
                    i.StatusId = (ItemStatus)statusId;
                    i.CostPerUnit = costPerUnit;
                    i.Postage = postage;
                    i.ActualCost = actualCost;

                    db.SaveChanges();
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {             
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddInstallationEquipmentLabour(int installationId, int equipmentLabourId)
        {
            try
            {
                Installation i = db.Installations.Find(installationId);
                EquipmentLabour el = db.EquipmentLabours.Find(equipmentLabourId);

                if (i != null && el != null && i.EquipmentId == el.EquipmentId)
                {
                    InstallationEquipmentLabour iei = new InstallationEquipmentLabour()
                    {
                        Installation = i,
                        EquipmentLabour = el,
                        LabourStatusId = LabourStatus.None
                    };
                    i.InstallationEquipmentLabours.Add(iei);
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
            ViewBag.Statuses = ExtensionMethods.AsSelectList<ItemStatus>();
            return View(installation);
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
