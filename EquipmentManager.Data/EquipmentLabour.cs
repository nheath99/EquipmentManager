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
    
    public partial class EquipmentLabour
    {
        public EquipmentLabour()
        {
            this.InstallationEquipmentLabours = new HashSet<InstallationEquipmentLabour>();
        }
    
        public int Id { get; set; }
        public int EquipmentModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public double Quantity { get; set; }
        public TemporalUnit QuantityUnit { get; set; }
        public System.DateTime ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }
    
        public virtual EquipmentModule EquipmentModule { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<InstallationEquipmentLabour> InstallationEquipmentLabours { get; set; }
    }
}
