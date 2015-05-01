using EquipmentManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquipmentManager.Controllers
{
    public class UnitsController : Controller
    {
        EquipmentManagerEntities db = new EquipmentManagerEntities();

        public ActionResult Index()
        {
            var units = db.UnitsOfMeasures.ToList();
            return View(units);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var unit = db.UnitsOfMeasures.Find(id.Value);
            if (unit == null)
            {
                return HttpNotFound();
            }

            return View(unit);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Name")] UnitsOfMeasure unit)
        {
            if (ModelState.IsValid)
            {
                db.UnitsOfMeasures.Add(unit);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(unit);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var unit = db.UnitsOfMeasures.Find(id.Value);
            if (unit == null)
            {
                return HttpNotFound();
            }

            return View(unit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name")] UnitsOfMeasure unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unit).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(unit);
        }
    }
}