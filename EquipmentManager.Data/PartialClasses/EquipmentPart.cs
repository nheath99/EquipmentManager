using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    [MetadataType(typeof(IEquipmentPart))]
    public partial class EquipmentPart
    {
        public bool IsValid(DateTime date)
        {
            return this.ValidFrom.Date <= date.Date && (this.ValidTo == null || this.ValidTo.Value.Date > date.Date);
        }

        public bool IsValidToday
        {
            get { return this.IsValid(DateTime.Today); }
        }

        public string QuantityRequiredSpareString
        {
            get { return this.QuantityRequiredSpare != 0 ? string.Format("({0})", this.QuantityRequiredSpare) : string.Empty; }
        }

        public double TotalRequired
        {
            get { return this.QuantityRequired + this.QuantityRequiredSpare; }
        }
    }

    public interface IEquipmentPart
    {
        int Id { get; set; }
        int EquipmentModuleId { get; set; }
        int PartId { get; set; }
        double QuantityRequired { get; set; }
        double QuantityRequiredSpare { get; set; }
        string UnitOfMeasure { get; set; }
        string Notes { get; set; }
        DateTime ValidFrom { get; set; }
        Nullable<DateTime> ValidTo { get; set; }

        EquipmentModule EquipmentModule { get; set; }
        Part Part { get; set; }
    }
}
