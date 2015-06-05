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
    
    public partial class EquipmentModule
    {
        public EquipmentModule()
        {
            this.EquipmentLabours = new HashSet<EquipmentLabour>();
            this.SubordonateModules = new HashSet<EquipmentModule>();
            this.EquipmentParts = new HashSet<EquipmentPart>();
            this.InstallationEquipmentModules = new HashSet<InstallationEquipmentModule>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EquipmentId { get; set; }
        public Nullable<int> ParentModuleId { get; set; }
        public System.DateTime ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }
    
        public virtual Equipment Equipment { get; set; }
        public virtual ICollection<EquipmentLabour> EquipmentLabours { get; set; }
        public virtual ICollection<EquipmentModule> SubordonateModules { get; set; }
        public virtual EquipmentModule ParentModule { get; set; }
        public virtual ICollection<EquipmentPart> EquipmentParts { get; set; }
        public virtual ICollection<InstallationEquipmentModule> InstallationEquipmentModules { get; set; }
    }
}
