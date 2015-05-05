using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    [MetadataType(typeof(IItemCategory))]
    public partial class ItemCategory
    {
    }

    public interface IItemCategory
    {
        int Id { get; set; }
        [Required]
        string Name { get; set; }

        ICollection<Item> Items { get; set; }
    }
}
