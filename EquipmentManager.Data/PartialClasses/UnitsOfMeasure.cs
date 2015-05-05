using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    [MetadataType(typeof(IUnitsOfMeasure))]
    public partial class UnitsOfMeasure
    {
    }

    public interface IUnitsOfMeasure
    {
        int Id { get; set; }
        [Required]
        string Name { get; set; }
    }
}
