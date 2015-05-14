using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    public partial class Installation
    {
        public bool HasEquipmentItem(EquipmentItem equipmentItem)
        {
            return this.InstallationEquipmentItems.Count(x => x.EquipmentItemId == equipmentItem.Id) > 0;
        }

        public bool HasEquipmentLabour(EquipmentLabour equipmentLabour)
        {
            return this.InstallationEquipmentLabours.Count(x => x.EquipmentLabourId == equipmentLabour.Id) > 0;
        }

        public IEnumerable<EquipmentItem> OutstandingItems
        {
            get
            {
                return this.Equipment.EquipmentItems.Where(x => !this.HasEquipmentItem(x));
            }
        }

        public IEnumerable<EquipmentLabour> OutstandingLabour
        {
            get
            {
                return this.Equipment.EquipmentLabours.Where(x => !this.HasEquipmentLabour(x));
            }
        }

        public decimal TotalCostItems
        {
            get
            {
                return this.InstallationEquipmentItems.Sum(x => x.ActualCost ?? x.TotalCost ?? 0m);
            }
        }

        public decimal TotalCostLabour
        {
            get { return this.InstallationEquipmentLabours.Sum(x => x.ActualCost ?? x.TotalCost ?? 0m); }
        }
    }
}
