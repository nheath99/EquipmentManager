using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    public partial class EquipmentModule
    {
        public IDictionary<int, string> ModuleHierarchy
        {
            get
            {
                Dictionary<int, string> dict = new Dictionary<int, string>();
                if(this.ParentModule != null)
                {
                    foreach (var item in this.ParentModule.ModuleHierarchy)
                    {
                        dict.Add(item.Key, item.Value);
                    }
                }
                dict.Add(this.Id, this.Name);
                return dict;
            }
        }
    }
}
