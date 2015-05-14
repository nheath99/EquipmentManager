using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    public partial class InstallationEquipmentLabour
    {
        public decimal? TotalCost
        {
            get
            {
                if (this.Quantity.HasValue && this.CostPerUnit.HasValue)
                {
                    return Convert.ToDecimal(this.Quantity.Value) * this.CostPerUnit.Value;
                }
                else
                {
                    return null;
                }
            }
        }

        public double TotalMinutes
        {
            get
            {
                if (this.Quantity.HasValue)
                {
                    switch (this.QuantityUnit)
                    {
                        case TemporalUnit.Hours:
                            return this.Quantity.Value * 60;
                        case TemporalUnit.Days:
                            return this.Quantity.Value * 60 * 24;
                        case TemporalUnit.Weeks:
                            return this.Quantity.Value * 60 * 24 * 7;
                        case TemporalUnit.Months:
                            return this.Quantity.Value * 60 * 24 * 7 * 30;
                        case TemporalUnit.Years:
                            return this.Quantity.Value * 60 * 24 * 7 * 365;
                        default:
                            return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public string QuantityString
        {
            get { return this.Quantity.HasValue ? string.Format("{0} {1}", Math.Round(this.Quantity.Value, 2), this.QuantityUnit.Name()) : string.Empty; }
        }

        public string CostString
        {
            get { return this.CostPerUnit.HasValue && this.QuantityUnit.HasValue
                ? string.Format("{0} per {1}", Math.Round(this.CostPerUnit.Value, 2), this.QuantityUnit.Name())
                : string.Empty; }
        }
    }
}
