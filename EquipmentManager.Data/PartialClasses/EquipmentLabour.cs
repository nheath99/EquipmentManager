using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    public partial class EquipmentLabour
    {
        public bool IsValid(DateTime date)
        {
            return this.ValidFrom.Date <= date.Date && (this.ValidTo == null || this.ValidTo.Value.Date > date.Date);
        }

        public bool IsValidToday
        {
            get { return this.IsValid(DateTime.Today); }
        }

        public double TotalMinutes
        {
            get
            {
                switch (this.QuantityUnit)
                {
                    case TemporalUnit.Hours:
                        return this.Quantity * 60;
                    case TemporalUnit.Days:
                        return this.Quantity * 60 * 24;
                    case TemporalUnit.Weeks:
                        return this.Quantity * 60 * 24 * 7;
                    case TemporalUnit.Months:
                        return this.Quantity * 60 * 24 * 7 * 30;
                    case TemporalUnit.Years:
                        return this.Quantity * 60 * 24 * 7 * 365;
                    default:
                        return 0;
                }
            }
        }

        public string QuantityString
        {
            get { return string.Format("{0} {1}", Math.Round(this.Quantity, 2), this.QuantityUnit.Name()); }
        }

        public string SupplierName
        {
            get { return this.Supplier != null ? this.Supplier.Name : string.Empty; }
        }

        public InstallationEquipmentLabour InstallationLabour(InstallationEquipmentModule parentModule)
        {
            InstallationEquipmentLabour l = new InstallationEquipmentLabour()
            {
                EquipmentLabour = this,
                InstallationEquipmentModule = parentModule
            };
            return l;
        }
    }
}
