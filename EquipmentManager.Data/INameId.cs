using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    public interface INameId
    {
        int Id { get; }
        string Name { get; }
    }
}
