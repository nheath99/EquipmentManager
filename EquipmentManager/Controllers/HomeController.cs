using EquipmentManager.Data;
using EquipmentManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquipmentManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            EquipmentManagerEntities db = new EquipmentManagerEntities();

            var model = new HomeModel();

            var itemIds = Membership.RecentItemIds(User.Identity.GetUserName());
            if (itemIds != null)
                model.RecentParts = itemIds.Select(x => db.Parts.Find(x));
            
            var installationIds = Membership.RecentInstallationIds(User.Identity.GetUserName());
            if (installationIds != null)
                model.RecentInstallations = installationIds.Select(x => db.Installations.Find(x));

            var equipmentIds = Membership.RecentEquipmentIds(User.Identity.GetUserName());
            if (equipmentIds != null)
                model.RecentEquipment = equipmentIds.Select(x => db.Equipments.Find(x));
            
            return View(model);
        }

        public ActionResult Settings()
        {
            return View();
        }
    }
}