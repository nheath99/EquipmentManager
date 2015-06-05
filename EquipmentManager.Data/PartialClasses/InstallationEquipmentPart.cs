using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    [MetadataType(typeof(IInstallationEquipmentItem))]
    public partial class InstallationEquipmentPart : IInstallationEquipmentItem
    {
        public int InstallationId
        {
            get
            {
                return this.InstallationEquipmentModule.InstallationId;
            }
        }

        public string Name
        {
            get
            {
                if (this.EquipmentPart.Part.Obsolete)
                {
                    return string.Format("{0} - OBSOLETE", this.EquipmentPart.Part.Name);
                }
                else
                {
                    return this.EquipmentPart.Part.Name;
                }
            }                
        }

        public decimal Cost
        {
            get { return this.ActualCost ?? this.QuotedCost; }
        }

        public decimal QuotedCost
        {
            get { return ((this.CostPerUnit ?? 0) * (decimal)(this.UnitsOrdered ?? 0)) + (this.Postage ?? 0); }
        }
    }

    public interface IInstallationEquipmentItem
    {
        int Id { get; set; }
        [Display(Name="Module")]
        int InstallationModuleId { get; set; }
        [Display(Name="Equipment Part")]
        int EquipmentPartId { get; set; }
        [Display(Name="Date Quoted")]
        Nullable<System.DateTime> DateQuoted { get; set; }
        [Display(Name="Date Ordered")]
        Nullable<System.DateTime> DateOrdered { get; set; }
        [Display(Name="Date Expected")]
        Nullable<System.DateTime> DateExpected { get; set; }
        [Display(Name="Date Received")]
        Nullable<System.DateTime> DateReceived { get; set; }
        [Display(Name="Status")]
        InstallationPartStatus StatusId { get; set; }
        [Display(Name="Cost Per Unit")]
        Nullable<decimal> CostPerUnit { get; set; }
        [Display(Name="Units Ordered")]
        Nullable<double> UnitsOrdered { get; set; }
        [Display(Name="Postage")]
        Nullable<decimal> Postage { get; set; }
        [Display(Name="Actual Cost")]
        Nullable<decimal> ActualCost { get; set; }
        [Display(Name = "Notes")]
        string Notes { get; set; }
        
        [Display(Name="Installation Module")]
        InstallationEquipmentModule InstallationEquipmentModule { get; set; }
        [Display(Name="Equipment Part")]
        EquipmentPart EquipmentPart { get; set; }
    }
}
