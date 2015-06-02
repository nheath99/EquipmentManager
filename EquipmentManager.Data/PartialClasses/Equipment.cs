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

        /// <summary>
        /// Returns all active parts in the Equipment grouped by Part and Unit.
        /// </summary>
        public IEnumerable<EquipmentPartExtended> AllPartsCombined
        {
            get
            {
                var results = this.EquipmentModules.SelectMany(x => x.CurrentParts)
                    .GroupBy(x => new { x.Part, x.UnitOfMeasure })
                    .Select(x => new EquipmentPartExtended()
                    {
                        PartId = x.Key.Part.Id,
                        Part = x.Key.Part,
                        UnitOfMeasure = x.Key.UnitOfMeasure,
                        QuantityRequired = x.Sum(y => y.QuantityRequired),
                        QuantityRequiredSpare = x.Sum(y => y.QuantityRequiredSpare),
                        EquipmentModules = x.Select(y => y.EquipmentModule).Distinct(),
                        Lines = x.Count()
                    });

                return results;
            }
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
