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
        public string CodeName
        {
            get { return string.Format("{0} - {1}", this.Code, this.Name); }
        }
    }

    public interface ISite
    {
        int Id { get; set; }
        [Required]
        string Name { get; set; }

        ICollection<Installation> Installations { get; set; }
    }
}
