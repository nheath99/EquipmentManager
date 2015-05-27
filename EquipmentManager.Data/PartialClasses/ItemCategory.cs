using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    [MetadataType(typeof(IPartCategory))]
    public partial class PartCategory
    {
    }

    public interface IPartCategory
    {
        int Id { get; set; }
        [Required]
        string Name { get; set; }

        ICollection<Part> Parts { get; set; }
    }
}
