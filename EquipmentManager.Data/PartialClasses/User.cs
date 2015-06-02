using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Data
{
    public partial class User
    {
        public static readonly int RecentItemsToStore = 10;
        public static readonly int RecentInstallationsToStore = 10;
        public static readonly int RecentEquipmentToStore = 10;

        public static readonly char SplitChar = ',';

        public void RemovePart(int partId)
        {
            if (this.RecentItemIds.Contains(partId))
            {
                var partIds = this.RecentItemIds;
                partIds.Remove(partId);
                this.RecentItemIds = partIds;
            }
        }

        public void RemoveEquipment(int equipmentId)
        {
            if (this.RecentEquipmentIds.Contains(equipmentId))
            {
                var equipmentIds = this.RecentEquipmentIds;
                equipmentIds.Remove(equipmentId);
                this.RecentEquipmentIds = equipmentIds;
            }
        }

        public void RemoveInstallation(int installationId)
        {
            if (this.RecentInstallationIds.Contains(installationId))
            {
                var installationIds = this.RecentInstallationIds;
                installationIds.Remove(installationId);
                this.RecentInstallationIds = installationIds;
            }
        }

        public void AddRecentEquipment(int equipmentId)
        {
            if (string.IsNullOrEmpty(this.RecentEquipment))
            {
                this.RecentEquipment = equipmentId.ToString();
            }
            else
            {
                var equipment = this.RecentEquipment.Split(SplitChar);
                var equipmentIds = equipment.Select(x => Convert.ToInt32(x)).ToList();
                // remove the itemId if it exists                
                if (equipmentIds.Contains(equipmentId))
                {
                    equipmentIds.Remove(equipmentId);
                }
                // add it to the list
                equipmentIds.Add(equipmentId);
                // if the list is greater than 10, remove top item
                if (equipmentIds.Count > RecentEquipmentToStore)
                {
                    equipmentIds.RemoveAt(0);
                }
                this.RecentEquipment = string.Join(SplitChar.ToString(), equipmentIds);
            }
        }

        public void AddRecentInstallation(int installationId)
        {
            if (string.IsNullOrEmpty(this.RecentInstallations))
            {
                this.RecentInstallations = installationId.ToString();
            }
            else
            {
                var installations = this.RecentInstallations.Split(SplitChar);
                var installationIds = installations.Select(x => Convert.ToInt32(x)).ToList();
                // remove the itemId if it exists                
                if (installationIds.Contains(installationId))
                {
                    installationIds.Remove(installationId);
                }
                // add it to the list
                installationIds.Add(installationId);
                // if the list is greater than 10, remove top item
                if (installationIds.Count > RecentInstallationsToStore)
                {
                    installationIds.RemoveAt(0);
                }
                this.RecentInstallations = string.Join(SplitChar.ToString(), installationIds);
            }
        }

        public void AddRecentItem(int itemId)
        {
            if (string.IsNullOrEmpty(this.RecentItems))
            {
                this.RecentItems = itemId.ToString();
            }
            else
            {
                var items = this.RecentItems.Split(SplitChar);
                var itemIds = items.Select(x => Convert.ToInt32(x)).ToList();

                if (itemIds.Contains(itemId))
                {
                    itemIds.Remove(itemId);
                }

                itemIds.Add(itemId);

                while (itemIds.Count > RecentItemsToStore)
                {
                    itemIds.RemoveAt(0);
                }

                this.RecentItems = string.Join(SplitChar.ToString(), itemIds);
            }
        }

        public List<int> RecentItemIds
        {
            get
            {
                if (!string.IsNullOrEmpty(this.RecentItems))
                    return this.RecentItems.Split(SplitChar).Select(x => Convert.ToInt32(x)).Reverse().ToList();
                else
                    return new List<int>();
            }
            set
            {
                this.RecentItems = string.Join(SplitChar.ToString(), value);
            }
        }

        public List<int> RecentInstallationIds
        {
            get
            {
                if (!string.IsNullOrEmpty(this.RecentInstallations))
                    return this.RecentInstallations.Split(SplitChar).Select(x => Convert.ToInt32(x)).Reverse().ToList();
                else
                    return new List<int>();
            }
            set
            {
                this.RecentInstallations = string.Join(SplitChar.ToString(), value);
            }
        }

        public List<int> RecentEquipmentIds
        {
            get
            {
                if (!string.IsNullOrEmpty(this.RecentEquipment))
                    return this.RecentEquipment.Split(SplitChar).Select(x => Convert.ToInt32(x)).Reverse().ToList();
                else
                    return new List<int>();
            }
            set
            {
                this.RecentEquipment = string.Join(SplitChar.ToString(), value);
            }
        }
    }
}
