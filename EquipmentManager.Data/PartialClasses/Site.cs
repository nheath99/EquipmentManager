using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    [MetadataType(typeof(ISite))]
    public partial class Site
    {
    }

    public interface ISite
    {
        int Id { get; set; }
        [Required]
        string Name { get; set; }

        ICollection<Installation> Installations { get; set; }
    }
}
