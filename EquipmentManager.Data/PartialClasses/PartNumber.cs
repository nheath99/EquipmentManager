using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    [MetadataType(typeof(IPartNumber))]
    public partial class PartNumber
    {
    }

    public interface IPartNumber
    {
        int Id { get; set; }
        [Required]
        int ItemId { get; set; }
        [Required]
        string Value { get; set; }
        string Description { get; set; }
        bool Active { get; set; }

        Item Item { get; set; }
    }
}
