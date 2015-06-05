using EquipmentManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EquipmentManager.Models
{
    public class EquipmentViewModel
    {
        public EquipmentViewModel()
        {
            this.EquipmentModules = new HashSet<EquipmentModule>();
            this.Labour = new HashSet<EquipmentLabour>();
            this.AllPartsCombined = new HashSet<EquipmentPartExtended>();
        }

        public EquipmentViewModel(Equipment e)
            : this()
        {
            this.Id = e.Id;
            this.Name = e.Name;
            this.Description = e.Description;

            foreach (var item in e.EquipmentModules)
            {
                this.EquipmentModules.Add(item);
            }

            this.Installations = e.Installations;
            this.AllPartsCombined = e.AllPartsCombined;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<EquipmentModule> EquipmentModules { get; set; }
        public ICollection<Installation> Installations { get; set; }
        public ICollection<EquipmentLabour> Labour { get; set; }

        public IEnumerable<EquipmentModule> TopLevelModules
        {
            get { return this.EquipmentModules.Where(x => x.ParentModuleId == null); }
        }

        public IEnumerable<EquipmentModule> CurrentTopLevelModules
        {
            get { return this.EquipmentModules.Where(x => x.ParentModuleId == null && x.IsValidToday); }
        }

        public IEnumerable<EquipmentPartExtended> AllPartsCombined { get; private set; }
    }

    public class EquipmentPartViewModel
    {
        public EquipmentPartViewModel()
        {
        }

        public EquipmentPartViewModel(EquipmentPart e)
        {
            this.Id = e.Id;
            this.EquipmentCompositeId = e.EquipmentModuleId;
            this.ItemId = e.PartId;
            this.QuantityRequired = e.QuantityRequired;
            this.UnitOfMeasure = e.UnitOfMeasure;
            this.Notes = e.Notes;
            this.ValidFrom = e.ValidFrom;
            this.ValidTo = e.ValidTo;
            this.IsValidToday = e.IsValidToday;

            if (e.Part != null)
            {
                this.ItemName = e.Part.Name;
                this.ItemDescription = e.Part.Description;
                this.ItemsPerUnit = e.Part.ItemsPerUnit;
                this.ItemLink = e.Part.Link;
                this.ItemObsolete = e.Part.Obsolete;
                this.PartCategory = e.Part.PartCategory;
                this.PartManufacturer = e.Part.Manufacturer;
                this.ItemSupplier = e.Part.Supplier;
                this.ItemPartNumbers = e.Part.PartNumbers;
            }
        }

        public int Id { get; set; }
        public int EquipmentCompositeId { get; set; }
        public int ItemId { get; set; }
        public double QuantityRequired { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Notes { get; set; }
        public DateTime ValidFrom { get; set; }
        public Nullable<DateTime> ValidTo { get; set; }
        public bool IsValidToday { get; set; }

        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal ItemsPerUnit { get; set; }
        public string ItemLink { get; set; }
        public bool ItemObsolete { get; set; }
        public PartCategory PartCategory { get; set; }
        public Manufacturer PartManufacturer { get; set; }
        public Supplier ItemSupplier { get; set; }
        [Display(Name = "Part Numbers")]
        public ICollection<PartNumber> ItemPartNumbers { get; set; }

        public string PartManufacturerName
        { get { return this.PartManufacturer != null ? this.PartManufacturer.Name : string.Empty; } }

        public string PartSupplierName
        { get { return this.ItemSupplier != null ? this.ItemSupplier.Name : string.Empty; } }

        public string PartCategoryName
        { get { return this.PartCategory != null ? this.PartCategory.Name : string.Empty; } }
    }
}