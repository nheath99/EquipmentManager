using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    [MetadataType(typeof(IEquipment))]
    public partial class Equipment
    {
    }

    public interface IEquipment
    {
        int Id { get; set; }
        [Required]
        string Name { get; set; }
        string Description { get; set; }

        ICollection<EquipmentItem> EquipmentItems { get; set; }
        ICollection<Installation> Installations { get; set; }
    }
}
