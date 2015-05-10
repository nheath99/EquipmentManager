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
            this.EquipmentItems = new HashSet<EquipmentItemViewModel>();
            this.Labour = new HashSet<EquipmentLabour>();
        }

        public EquipmentViewModel(Equipment e)
            : this()
        {
            this.Id = e.Id;
            this.Name = e.Name;
            this.Description = e.Description;

            foreach (var item in e.EquipmentItems)
            {
                this.EquipmentItems.Add(new EquipmentItemViewModel(item));
            }

            foreach (var item in e.EquipmentLabours)
            {
                this.Labour.Add(item);
            }

            this.Installations = e.Installations;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<EquipmentItemViewModel> EquipmentItems { get; set; }
        public ICollection<Installation> Installations { get; set; }
        public ICollection<EquipmentLabour> Labour { get; set; }
    }

    public class EquipmentItemViewModel
    {
        public EquipmentItemViewModel()
        {
        }

        public EquipmentItemViewModel(EquipmentItem e)
        {
            this.Id = e.Id;
            this.EquipmentId = e.EquipmentId;
            this.ItemId = e.ItemId;
            this.QuantityRequired = e.QuantityRequired;
            this.QuantityRequiredSpare = e.QuantityRequiredSpare;
            this.UnitOfMeasure = e.UnitOfMeasure;
            this.Notes = e.Notes;
            this.ValidFrom = e.ValidFrom;
            this.ValidTo = e.ValidTo;
            this.IsValidToday = e.IsValidToday;

            if (e.Item != null)
            {
                this.ItemName = e.Item.Name;
                this.ItemDescription = e.Item.Description;
                this.ItemsPerUnit = e.Item.ItemsPerUnit;
                this.ItemLink = e.Item.Link;
                this.ItemObsolete = e.Item.Obsolete;
                this.ItemCategory = e.Item.ItemCategory;
                this.ItemManufacturer = e.Item.Manufacturer;
                this.ItemSupplier = e.Item.Supplier;
                this.ItemPartNumbers = e.Item.PartNumbers;
            }
        }

        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int ItemId { get; set; }
        public double QuantityRequired { get; set; }
        public double QuantityRequiredSpare { get; set; }
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
        public ItemCategory ItemCategory { get; set; }
        public Manufacturer ItemManufacturer { get; set; }
        public Supplier ItemSupplier { get; set; }
        [Display(Name = "Part Numbers")]
        public ICollection<PartNumber> ItemPartNumbers { get; set; }

        public string ItemManufacturerName
        { get { return this.ItemManufacturer != null ? this.ItemManufacturer.Name : string.Empty; } }

        public string ItemSupplierName
        { get { return this.ItemSupplier != null ? this.ItemSupplier.Name : string.Empty; } }

        public string ItemCategoryName
        { get { return this.ItemCategory != null ? this.ItemCategory.Name : string.Empty; } }

        public string QuantityRequiredSpareString
        {
            get
            {
                if (this.QuantityRequiredSpare == 0)
                {
                    return string.Empty;
                }
                else
                {
                    return string.Format("({0})", this.QuantityRequiredSpare);
                }
            }
        }
    }
}