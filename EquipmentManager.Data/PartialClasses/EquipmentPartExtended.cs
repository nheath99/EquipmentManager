using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    public class EquipmentPartExtended : EquipmentPart
    {
        public IEnumerable<EquipmentModule> EquipmentModules { get; set; }
        public int Lines { get; set; }
    }
}
