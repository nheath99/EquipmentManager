using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    [MetadataType(typeof(IInstallationEquipmentModule))]
    public partial class InstallationEquipmentModule
    {
        public string Name
        { 
            get { return this.EquipmentModule.Name; }
        }

        public decimal Cost
        {
            get
            {
                return this.ActualCost ??
                    this.PartsCost + this.LabourCost + this.SubordonateModules.Sum(x => x.Cost);
            }
        }

        public decimal QuotedCost
        {
            get { return this.QuotedPartsCost + this.QuotedLabourCost + this.SubordonateModules.Sum(x => x.QuotedCost); }
        }

        public decimal PartsCost
        {
            get { return this.InstallationEquipmentParts.Sum(x => x.Cost); }
        }

        public decimal QuotedPartsCost
        {
            get { return this.InstallationEquipmentParts.Sum(x => x.QuotedCost); }
        }

        public decimal LabourCost
        {
            get { return this.InstallationEquipmentLabours.Sum(x => x.Cost); }
        }

        public decimal QuotedLabourCost
        {
            get { return this.InstallationEquipmentLabours.Sum(x => x.QuotedCost); }
        }

        public decimal RecursivePartsCost
        {
            get { return this.PartsCost + this.SubordonateModules.Sum(x => x.RecursivePartsCost); }
        }

        public decimal RecursiveLabourCost
        {
            get { return this.LabourCost + this.SubordonateModules.Sum(x => x.RecursiveLabourCost); }
        }

        public decimal RecursiveQuotedPartsCost
        {
            get { return this.QuotedPartsCost + this.SubordonateModules.Sum(x => x.RecursiveQuotedPartsCost); }
        }

        public decimal RecursiveQuotedLabourCost
        {
            get { return this.QuotedLabourCost + this.SubordonateModules.Sum(x => x.RecursiveQuotedLabourCost); }
        }

        public IDictionary<int, string> ModuleHierarchy
        {
            get
            {
                Dictionary<int, string> dict = new Dictionary<int, string>();
                if (this.ParentModule != null)
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

        /// <summary>
        /// Returns the level in the module hierarchy where 1 is the top level.
        /// </summary>
        public int Level
        {
            get
            {
                if (this.ParentModuleId == null)
                {
                    return 1;
                }
                else
                {
                    return this.ParentModule.Level + 1;
                }
            }
        }
    }

    public interface IInstallationEquipmentModule
    {
        int Id { get; set; }
        int EquipmentModuleId { get; set; }
        int InstallationId { get; set; }
        [Display(Name="Actual Cost")]
        Nullable<decimal> ActualCost { get; set; }
        [Display(Name="Cost")]
        decimal Cost { get; }
        Nullable<int> ParentModuleId { get; set; }

        ICollection<InstallationEquipmentLabour> InstallationEquipmentLabours { get; set; }
        Installation Installation { get; set; }
        ICollection<InstallationEquipmentPart> InstallationEquipmentParts { get; set; }
        [Display(Name="Subordonate Modules")]
        ICollection<InstallationEquipmentModule> SubordonateModules { get; set; }
        [Display(Name="Parent Module")]
        InstallationEquipmentModule ParentModule { get; set; }
        EquipmentModule EquipmentModule { get; set; }
    }
}
