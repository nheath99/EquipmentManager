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
        public IEnumerable<EquipmentModule> TopLevelModules
        {
            get { return this.EquipmentModules.Where(x => x.ParentModuleId == null); }
        }
    }

    public interface IEquipment
    {
        int Id { get; set; }
        [Required]
        string Name { get; set; }
        string Description { get; set; }

        ICollection<Installation> Installations { get; set; }
        ICollection<EquipmentModule> EquipmentModules { get; set; }
    }
}
