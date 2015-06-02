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
            ViewBag.Statuses = EnumHelpers.AsSelectList<ItemStatus>();
            ViewBag.LabourStatuses = EnumHelpers.AsSelectList<LabourStatus>();
            ViewBag.Units = EnumHelpers.AsSelectList<TemporalUnit>();
            Membership.AddRecentInstallationToUser(User.Identity.GetUserName(), installation.Id);
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
