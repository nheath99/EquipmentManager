using EquipmentManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EquipmentManager
{
    public class Membership
    {
        public static bool IsUserInDatabase(string userName)
        {
            EquipmentManagerEntities db = new EquipmentManagerEntities();
            var user = db.Users.SingleOrDefault(x => x.UserName == userName);
            return user != null;
        }

        public static User GetOrCreateUser(string userName)
        {
            EquipmentManagerEntities db = new EquipmentManagerEntities();
            User u = db.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
            if (u == null)
            {
                u = new User()
                {
                    UserName = userName
                };
                db.Users.Add(u);
                db.SaveChanges();
            }
            return u;
        }

        public static void AddRecentPartToUser(string userName, int partId)
        {
            EquipmentManagerEntities db = new EquipmentManagerEntities();
            var user = db.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
            user.AddRecentItem(partId);
            db.SaveChanges();
        }

        public static List<int> RecentItemIds(string userName)
        {
            EquipmentManagerEntities db = new EquipmentManagerEntities();
            var user = db.Users.SingleOrDefault(x => x.UserName == userName);
            return user.RecentItemIds;
        }

        public static void AddRecentInstallationToUser(string userName, int installationId)
        {
            EquipmentManagerEntities db = new EquipmentManagerEntities();
            var user = db.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
            user.AddRecentInstallation(installationId);
            db.SaveChanges();
        }

        public static void AddRecentEquipmentToUser(string userName, int equipmentId)
        {
            EquipmentManagerEntities db = new EquipmentManagerEntities();
            var user = db.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
            user.AddRecentEquipment(equipmentId);
            db.SaveChanges();
        }

        public static List<int> RecentInstallationIds(string userName)
        {
            EquipmentManagerEntities db = new EquipmentManagerEntities();
            var user = db.Users.SingleOrDefault(x => x.UserName == userName);
            return user.RecentInstallationIds;
        }

        public static List<int> RecentEquipmentIds(string userName)
        {
            EquipmentManagerEntities db = new EquipmentManagerEntities();
            var user = db.Users.SingleOrDefault(x => x.UserName == userName);
            return user.RecentEquipmentIds;
        }
    }
}