using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    [MetadataType(typeof(ISupplier))]
    public partial class Supplier
    {
    }

    public interface ISupplier
    {
        int Id { get; set; }
        [Required]
        string Name { get; set; }
        string Website { get; set; }
        [Display(Name="Address 1")]
        string Address1 { get; set; }
        string Address2 { get; set; }
        string Address3 { get; set; }
        string Address4 { get; set; }
        string Postcode { get; set; }
        string Country { get; set; }

        ICollection<Item> Items { get; set; }
    }
}
