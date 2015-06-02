using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    public partial class InstallationEquipmentPart
    {
        public string Name
        {
            get { return this.EquipmentPart.Part.Name; }
        }

        public decimal Cost
        {
            get { return this.ActualCost ?? this.QuotedCost; }
        }

        public decimal QuotedCost
        {
            get { return (this.CostPerUnit ?? 0) * (decimal)(this.UnitsOrdered ?? 0) + this.Postage ?? 0; }
        }
    }
}
