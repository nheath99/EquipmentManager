using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    public partial class EquipmentModule
    {
        public IEnumerable<EquipmentModule> CurrentSubordonateModules
        {
            get
            {
                return this.SubordonateModules.Where(x => x.IsValidToday);
            }
        }

        public IEnumerable<EquipmentLabour> CurrentLabour
        {
            get
            {
                return this.EquipmentLabours.Where(x => x.IsValidToday);
            }
        }

        public bool IsValid(DateTime date)
        {
            return this.ValidFrom.Date <= date.Date && (this.ValidTo == null || this.ValidTo.Value.Date > date.Date);
        }

        public bool IsValidToday
        {
            get { return this.IsValid(DateTime.Today); }
        }

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

        public IEnumerable<EquipmentPart> CurrentParts
        {
            get
            {
                return this.EquipmentParts.Where(x => x.IsValidToday);
            }
        }

        public IEnumerable<EquipmentPart> AllParts
        {
            get
            {
                // need to group by Part and EquipmentPart.UnitOfMeasure

                var results = this.EquipmentParts
                    .GroupBy(x => new { x.Part, x.UnitOfMeasure })
                    .Select(x => new EquipmentPart
                    {
                        Part = x.Key.Part,
                        UnitOfMeasure = x.Key.UnitOfMeasure,
                        QuantityRequired = x.Sum(y => y.QuantityRequired)
                    });

                return results;
            }
        }

        public InstallationEquipmentModule InstallationModule(Installation installation, InstallationEquipmentModule parentModule)
        {
            InstallationEquipmentModule m = new InstallationEquipmentModule()
            {
                Installation = installation,
                ParentModule = parentModule,
                EquipmentModule = this
            };
            foreach (var module in this.SubordonateModules)
            {
                m.SubordonateModules.Add(module.InstallationModule(installation, m));
            }

            foreach (var part in this.CurrentParts)
            {
                m.InstallationEquipmentParts.Add(part.InstallationPart(m));
            }

            foreach (var labour in this.EquipmentLabours)
            {
                m.InstallationEquipmentLabours.Add(labour.InstallationLabour(m));
            }

            return m;
        }
    }
}
