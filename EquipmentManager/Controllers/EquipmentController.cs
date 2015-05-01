﻿using EquipmentManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquipmentManager.Controllers
{
    public class EquipmentController : Controller
    {
        EquipmentManagerEntities db = new EquipmentManagerEntities();

        public ActionResult Index()
        {
            var equipment = db.Equipments.ToList();
            return View(equipment);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var equipment = db.Equipments.Find(id.Value);
            if (equipment == null)
            {
                return HttpNotFound();
            }

            return View(equipment);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Name,Description")] Equipment eq)
        {
            if (ModelState.IsValid)
            {
                db.Equipments.Add(eq);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(eq);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var equipment = db.Equipments.Find(id.Value);
            if (equipment == null)
            {
                return HttpNotFound();
            }

            return View(equipment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Description")] Equipment eq)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eq).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eq);
        }
    }
}