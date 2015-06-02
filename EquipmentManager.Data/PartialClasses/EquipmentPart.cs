﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    [MetadataType(typeof(IEquipmentPart))]
    public partial class EquipmentPart
    {
        public bool IsValid(DateTime date)
        {
            return this.ValidFrom.Date <= date.Date && (this.ValidTo == null || this.ValidTo.Value.Date > date.Date);
        }

        public bool IsValidToday
        {
            get { return this.IsValid(DateTime.Today); }
        }

        public string QuantityString
        {
            get { return string.Format("{0} {1}", this.QuantityRequired, this.QuantityRequiredSpareString); }
        }

        public string QuantityRequiredSpareString
        {
            get { return this.QuantityRequiredSpare != 0 ? string.Format("({0})", this.QuantityRequiredSpare) : string.Empty; }
        }

        public double TotalRequired
        {
            get { return this.QuantityRequired + this.QuantityRequiredSpare; }
        }
        
        public InstallationEquipmentPart InstallationPart(InstallationEquipmentModule parentModule)
        {
            InstallationEquipmentPart p = new InstallationEquipmentPart()
            {
                InstallationEquipmentModule = parentModule,
                EquipmentPart = this
            };
            return p;
        }
    }

    public interface IEquipmentPart
    {
        int Id { get; set; }
        int EquipmentModuleId { get; set; }
        int PartId { get; set; }
        [Display(Name="Quantity Required")]
        double QuantityRequired { get; set; }
        [Display(Name = "Quantity Required Spare")]
        double QuantityRequiredSpare { get; set; }
        [Display(Name = "Unit of Measure")]
        string UnitOfMeasure { get; set; }
        string Notes { get; set; }
        [Display(Name = "Valid From")]
        DateTime ValidFrom { get; set; }
        [Display(Name = "Valid To")]
        Nullable<DateTime> ValidTo { get; set; }
        
        [Display(Name = "Module")]
        EquipmentModule EquipmentModule { get; set; }
        Part Part { get; set; }
    }
}
