using EquipmentManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquipmentManager.Controllers
{
    public class ManufacturersController : Controller
    {
        EquipmentManagerEntities db = new EquipmentManagerEntities();

        public ActionResult Index()
        {
            var suppliers = db.Manufacturers.ToList();
            return View(suppliers);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var manufacturer = db.Manufacturers.Find(id.Value);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }

            return View(manufacturer);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Website,Address1,Address2,Address3,Address4,Postcode,Country")] Manufacturer m)
        {
            if (ModelState.IsValid)
            {
                db.Manufacturers.Add(m);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(m);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var manufacturer = db.Manufacturers.Find(id.Value);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }

            return View(manufacturer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Website,Address1,Address2,Address3,Address4,Postcode,Country")] Manufacturer m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(m);
        }
    }
}