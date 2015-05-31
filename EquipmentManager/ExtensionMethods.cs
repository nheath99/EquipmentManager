using EquipmentManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EquipmentManager
{
    public static class EnumHelpers
    {
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
    }

    public static class ExtensionMethods
    {
        public static string GetUserName(this IIdentity identity)
        {
            string userName = identity.Name;
            while (userName.Contains('\\'))
            {
                userName = userName.Substring(1);
            }
            return userName;
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

        public static string Repeat(this string input, int number)
        {
            if (number < 1)
            {
                return string.Empty;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < number; i++)
                {
                    sb.Append(input);
                }
                return sb.ToString();
            }
        }

        public static string Repeat(this char input, int number)
        {
            return input.ToString().Repeat(number);
        }

        public static readonly char LevelIndicator = '-';

        public static IEnumerable<SelectListItem> ModuleList(this Equipment e, int? selectedModuleId = null, bool includeCurrentModule = true, int? currentModuleId = null)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (EquipmentModule module in e.TopLevelModules)
            {
                if (includeCurrentModule || (currentModuleId.HasValue && currentModuleId.Value != module.Id))
                {
                    list.Add(new SelectListItem() { Value = module.Id.ToString(), Text = module.Name, Selected = selectedModuleId.HasValue && selectedModuleId.Value == module.Id });
                    module.ChildModuleList(selectedModuleId, ref list, 1, includeCurrentModule, currentModuleId);
                }
            }
            return list;
        }

        public static void ChildModuleList(this EquipmentModule em, int? selectedModuleId, ref List<SelectListItem> list, int level = 0, bool includeCurrentModule = true, int? currentModuleId = null)
        {
            string levelIndicator = LevelIndicator.Repeat(level);
            foreach (EquipmentModule module in em.SubordonateModules)
            {
                if (includeCurrentModule || (currentModuleId.HasValue && currentModuleId.Value != module.Id))
                {
                    list.Add(new SelectListItem() { Value = module.Id.ToString(), Text = String.Format("{0}{1}", levelIndicator, module.Name), Selected = selectedModuleId.HasValue && selectedModuleId.Value == module.Id });
                    module.ChildModuleList(selectedModuleId, ref list, level + 1, includeCurrentModule, currentModuleId);
                }
            }
        }
    }
}