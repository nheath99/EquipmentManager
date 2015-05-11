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
            return this.InstallationEquipmentItems.Count(x => x.EquipmentItemId == equipmentItem.Id && x.UnitsOrdered >= equipmentItem.TotalRequired) > 0;
        }

        public IEnumerable<EquipmentItem> OutstandingItems
        {
            get
            {
                return this.Equipment.EquipmentItems.Where(x => !this.HasEquipmentItem(x));
            }
        }
    }
}
