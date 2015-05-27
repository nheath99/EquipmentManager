using EquipmentManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EquipmentManager.Models
{
    public class HomeModel
    {
        public HomeModel()
        {
            this.RecentParts = new HashSet<Part>();
            this.RecentInstallations = new HashSet<Installation>();
            this.RecentEquipment = new HashSet<Equipment>();
        }

        public IEnumerable<Part> RecentParts { get; set; }
        public IEnumerable<Installation> RecentInstallations { get; set; }
        public IEnumerable<Equipment> RecentEquipment { get; set; }
    }
}