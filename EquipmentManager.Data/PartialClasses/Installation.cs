using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    public partial class Installation
    {
        public bool HasEquipmentSection(EquipmentModule equipmentSection)
        {
            //return this.InstallationEquipmentComposites.Count(x => x.EquipmentSectionId == equipmentSection.Id) > 0;
            return false;
        }

        public IEnumerable<EquipmentModule> OutstandingSections
        {
            get
            {
                return this.Equipment.EquipmentModules.Where(x => !this.HasEquipmentSection(x));
            }
        }

        public decimal ActualCost
        {
            get
            {
                //return this.InstallationEquipmentSections.Sum(x =>
                //    x.ActualCost ??
                //    x.RealCost);
                return 0;
            }
        }        
    }
}
