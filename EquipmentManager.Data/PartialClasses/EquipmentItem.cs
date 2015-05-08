using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    [MetadataType(typeof(IEquipmentItem))]
    public partial class EquipmentItem
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
    }

    public interface IEquipmentItem
    {
        int Id { get; set; }
        int EquipmentId { get; set; }
        int ItemId { get; set; }
        double QuantityRequired { get; set; }
        double QuantityRequiredSpare { get; set; }
        string UnitOfMeasure { get; set; }
        string Notes { get; set; }
        DateTime ValidFrom { get; set; }
        Nullable<DateTime> ValidTo { get; set; }

        Equipment Equipment { get; set; }
        ICollection<InstallationEquipmentItem> InstallationEquipmentItems { get; set; }
        Item Item { get; set; }
    }
}
