﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    public enum ItemStatus
    {
        None,
        [Display(Name="Quote Received")]
        QuoteReceived,
        [Display(Name = "On Order")]
        OnOrder,
        Delivered,
        [Display(Name = "In Stock")]
        InStock,
        [Display(Name = "Not Required")]
        NotRequired
    }
}