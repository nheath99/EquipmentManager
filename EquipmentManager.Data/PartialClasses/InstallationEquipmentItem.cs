using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    [MetadataType(typeof(IInstallationEquipmentItem))]
    public partial class InstallationEquipmentItem
    {
        public decimal? TotalCost
        {
            get
            {
                if (CostPerUnit.HasValue && UnitsOrdered.HasValue)
                {
                    return (CostPerUnit.Value * Convert.ToDecimal(UnitsOrdered.Value)) + (Postage ?? 0m);
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public interface IInstallationEquipmentItem
    {
        int Id { get; set; }
        [Required]
        int InstallationId { get; set; }
        [Required]
        int EquipmentItemId { get; set; }
        Nullable<DateTime> DateOrdered { get; set; }
        Nullable<DateTime> DateExpected { get; set; }
        Nullable<DateTime> DateReceived { get; set; }
        Nullable<int> StatusId { get; set; }
        decimal CostPerUnit { get; set; }
        double UnitsOrdered { get; set; }
        decimal Postage { get; set; }

        EquipmentItem EquipmentItem { get; set; }
        Installation Installation { get; set; }
        ICollection<Order> Orders { get; set; }
    }
}
