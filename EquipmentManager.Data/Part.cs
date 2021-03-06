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
    
    public partial class Part
    {
        public Part()
        {
            this.PartNumbers = new HashSet<PartNumber>();
            this.Replaces = new HashSet<Part>();
            this.EquipmentParts = new HashSet<EquipmentPart>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public Nullable<int> ManufacturerId { get; set; }
        public decimal ItemsPerUnit { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Link { get; set; }
        public bool Obsolete { get; set; }
        public Nullable<int> ReplacedBy_Id { get; set; }
    
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual PartCategory PartCategory { get; set; }
        public virtual ICollection<PartNumber> PartNumbers { get; set; }
        public virtual ICollection<Part> Replaces { get; set; }
        public virtual Part ReplacedBy { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<EquipmentPart> EquipmentParts { get; set; }
    }
}
