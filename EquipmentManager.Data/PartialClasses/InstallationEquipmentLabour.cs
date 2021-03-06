﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    public partial class InstallationEquipmentLabour
    {
        public string QuantityString
        {
            get { return string.Format("{0} {1}", this.Quantity, this.QuantityUnit.Name()); }
        }

        public decimal Cost
        {
            get { return this.ActualCost ?? this.QuotedCost; }
        }

        public decimal QuotedCost
        {
            get { return ((this.CostPerUnit ?? 0) * (decimal)(this.Quantity ?? 0)); }
        }
    }
}
