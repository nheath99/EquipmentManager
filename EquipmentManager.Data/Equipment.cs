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
    
    public partial class Equipment
    {
        public Equipment()
        {
            this.Installations = new HashSet<Installation>();
            this.EquipmentModules = new HashSet<EquipmentModule>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Installation> Installations { get; set; }
        public virtual ICollection<EquipmentModule> EquipmentModules { get; set; }
    }
}
