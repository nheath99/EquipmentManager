//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EquipmentManager.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class EquipmentPart
    {
        public int Id { get; set; }
        public int EquipmentModuleId { get; set; }
        public int PartId { get; set; }
        public double QuantityRequired { get; set; }
        public double QuantityRequiredSpare { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Notes { get; set; }
        public System.DateTime ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }
    
        public virtual EquipmentModule EquipmentModule { get; set; }
        public virtual Part Part { get; set; }
    }
}