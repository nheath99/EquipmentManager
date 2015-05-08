using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    [MetadataType(typeof(IItem))]
    public partial class Item
    {
        public static readonly int LinkShortLength = 20;

        public string ManufacturerName
        { get { return this.Manufacturer != null ? this.Manufacturer.Name : string.Empty; } }

        public string SupplierName
        { get { return this.Supplier != null ? this.Supplier.Name : string.Empty; } }

        public string ItemCategoryName
        { get { return this.ItemCategory != null ? this.ItemCategory.Name : string.Empty; } }

        public string LinkShort
        {
            get
            {
                return this.Link != null ? (this.Link.Length <= LinkShortLength ? this.Link : this.Link.Substring(0, LinkShortLength - 2) + "...") : null;
            }
        }

        public string PartNumbersList
        {
            get
            {
                return string.Join(", ", this.PartNumbers.Select(x => x.Value));
            }
        }

        public string SearchString
        {
            get
            {
                List<string> l = new List<string>();

                l.Add(this.Name);
                if (this.Manufacturer != null)
                    l.Add(this.Manufacturer.Name);
                foreach (var pn in this.PartNumbers)
                {
                    l.Add(pn.Value);
                }

                return string.Join(" ", l);
            }
        }

        public string DisplayString
        {
            get
            {
                List<string> l = new List<string>();
                
                l.Add(this.Name);
                if (this.Manufacturer != null)
                    l.Add(this.Manufacturer.Name);
                foreach (var pn in this.PartNumbers)
                {
                    l.Add(pn.Value);
                }

                return string.Join(" - ", l);
            }
        }
    }

    public interface IItem
    {
        int Id { get; set; }
        [Required]
        string Name { get; set; }
        string Description { get; set; }
        Nullable<int> SupplierId { get; set; }
        Nullable<int> ManufacturerId { get; set; }
        [Display(Name="Items Per Unit")]
        decimal ItemsPerUnit { get; set; }
        Nullable<int> CategoryId { get; set; }
        string Link { get; set; }
        bool Obsolete { get; set; }
        Nullable<int> ReplacedBy_Id { get; set; }

        [Display(Name="Category")]
        ItemCategory ItemCategory { get; set; }
        ICollection<Item> Replaces { get; set; }
        [Display(Name="Replaced By")]
        Item ReplacedBy { get; set; }
        Manufacturer Manufacturer { get; set; }
        Supplier Supplier { get; set; }
        [Display(Name="Part Numbers")]
        ICollection<PartNumber> PartNumbers { get; set; }
        ICollection<EquipmentItem> EquipmentItems { get; set; }
    }
}
