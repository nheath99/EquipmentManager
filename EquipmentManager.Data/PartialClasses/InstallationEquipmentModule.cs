using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    public partial class InstallationEquipmentModule
    {
        public string Name
        { 
            get { return this.EquipmentModule.Name; }
        }

        public decimal Cost
        {
            get
            {
                return this.ActualCost ??
                    this.PartsCost + this.LabourCost + this.SubordonateModules.Sum(x => x.Cost);
            }
        }

        public decimal QuotedCost
        {
            get { return this.QuotedPartsCost + this.QuotedLabourCost + this.SubordonateModules.Sum(x => x.QuotedCost); }
        }

        public decimal PartsCost
        {
            get { return this.InstallationEquipmentParts.Sum(x => x.Cost); }
        }

        public decimal QuotedPartsCost
        {
            get { return this.InstallationEquipmentParts.Sum(x => x.QuotedCost); }
        }

        public decimal LabourCost
        {
            get { return this.InstallationEquipmentLabours.Sum(x => x.Cost); }
        }

        public decimal QuotedLabourCost
        {
            get { return this.InstallationEquipmentLabours.Sum(x => x.QuotedCost); }
        }
    }
}
