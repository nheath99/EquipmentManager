using EquipmentManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EquipmentManager.Models
{
    public class CreatePartViewModel
    {
        public CreatePartViewModel()
        {
            this.PartNumberDescriptions = new List<string>();
            this.PartNumberValues = new List<string>();
        }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public Nullable<int> ManufacturerId { get; set; }
        [Display(Name = "Items Per Unit")]
        [Required]
        public decimal ItemsPerUnit { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Link { get; set; }
        [Display(Name = "Part Numbers")]
        public List<string> PartNumberValues { get; set; }
        public List<string> PartNumberDescriptions { get; set; }

        public string Continue { get; set; }

        public Part GetPart()
        {
            Part p = new Part()
            {
                Name = this.Name,
                Description = this.Description,
                SupplierId = this.SupplierId,
                ManufacturerId = this.ManufacturerId,
                ItemsPerUnit = this.ItemsPerUnit,
                CategoryId = this.CategoryId,
                Link = this.Link
            };

            for (int j = 0; j < this.PartNumberValues.Count; j++)
            {
                p.PartNumbers.Add(new PartNumber()
                    {
                        Value = this.PartNumberValues[j],
                        Description = this.PartNumberDescriptions[j],
                        Active = true
                    });
            }

            return p;
        }
    }
}