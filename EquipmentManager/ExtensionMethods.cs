﻿using EquipmentManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquipmentManager
{
    public static class ExtensionMethods
    {
        public static string Name(this Enum e)
        {
            return (e as Enum).GetAttributeValue<DisplayAttribute>(x => x.Name);
        }
        
        public static string GetAttributeValue<T>(this Enum e, Func<T, object> selector) where T : Attribute
        {
            var output = e.ToString();
            var member = e.GetType().GetMember(output).First();
            var attributes = member.GetCustomAttributes(typeof(T), false);

            if (attributes.Length > 0)
            {
                var firstAttr = (T)attributes[0];
                var str = selector(firstAttr).ToString();
                output = string.IsNullOrWhiteSpace(str) ? output : str;
            }

            return output;
        }

        public static IEnumerable<SelectListItem> AsSelectList<T>(int? selectedItem = null)
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(x => new SelectListItem
                {
                    Text = (x as Enum).GetAttributeValue<DisplayAttribute>(y => y.Name),
                    Value = Convert.ToInt32(x).ToString(),
                    Selected = selectedItem.HasValue ? selectedItem == Convert.ToInt32(x) : false
                });
        }


        public static IEnumerable<SelectListItem> ConvertToSelectList<T, U>(this IEnumerable<IGrouping<T, U>> data, U selectedItem = null, IEnumerable<U> disabledItems = null)
            where T : INameId
            where U : class, INameId
        {
            List<SelectListItem> l = new List<SelectListItem>();
            foreach (var cat in data)
            {
                SelectListGroup group = new SelectListGroup()
                {
                    Name = cat.Key.Name
                };

                foreach (var item in cat)
                {
                    l.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = selectedItem != null ? selectedItem.Id == item.Id : false,
                        Group = group,
                        Disabled = disabledItems != null ? disabledItems.Select(x => x.Id).Contains(item.Id) : false
                    });
                }
            }

            return l;
        }
    }
}