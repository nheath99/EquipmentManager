using EquipmentManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquipmentManager.Controllers
{
    public class SuppliersController : Controller
    {
        EquipmentManagerEntities db = new EquipmentManagerEntities();

        public ActionResult Index()
        {
            var suppliers = db.Suppliers.ToList();
            return View(suppliers);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var supplier = db.Suppliers.Find(id.Value);
            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Website,Address1,Address2,Address3,Address4,Postcode,Country")] Supplier s)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(s);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(s);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var supplier = db.Suppliers.Find(id.Value);
            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Website,Address1,Address2,Address3,Address4,Postcode,Country")] Supplier s)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(s);
        }
    }
}