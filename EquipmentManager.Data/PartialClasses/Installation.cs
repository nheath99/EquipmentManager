using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    public partial class Installation
    {
        public IEnumerable<InstallationEquipmentModule> TopLevelModules
        {
            get { return this.InstallationEquipmentModules.Where(x => x.ParentModuleId == null); }
        }

        public bool HasEquipmentModule(EquipmentModule equipmentModule)
        {
            return this.InstallationEquipmentModules.Count(x => x.EquipmentModuleId == equipmentModule.Id) > 0;
        }

        public IEnumerable<EquipmentModule> OutstandingModules
        {
            get
            {
                return this.Equipment.EquipmentModules.Where(x => !this.HasEquipmentModule(x));
            }
        }

        public decimal Cost
        {
            get { return this.InstallationEquipmentModules.Sum(x => x.Cost); }
        }

        public decimal QuotedCost
        {
            get { return this.InstallationEquipmentModules.Sum(x => x.QuotedCost); }
        }

        public decimal PartsCost
        {
            get { return this.InstallationEquipmentModules.Sum(x => x.PartsCost); }
        }

        public decimal QuotedPartsCost
        {
            get { return this.InstallationEquipmentModules.Sum(x => x.QuotedPartsCost); }
        }

        public decimal LabourCost
        {
            get { return this.InstallationEquipmentModules.Sum(x => x.LabourCost); }
        }

        public decimal QuotedLabourCost
        {
            get { return this.InstallationEquipmentModules.Sum(x => x.QuotedLabourCost); }
        }

        public decimal DifferencePartsCost
        {
            get { return this.PartsCost - this.QuotedPartsCost; }
        }

        public decimal DifferenceLabourCost
        {
            get { return this.LabourCost - this.QuotedLabourCost; }
        }

        public decimal DifferenceCost
        {
            get { return this.DifferencePartsCost + this.DifferenceLabourCost; }
        }
    }
}
