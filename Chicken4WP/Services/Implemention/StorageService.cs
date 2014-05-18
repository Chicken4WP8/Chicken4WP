using System.Collections.Generic;
using System.Linq;
using Chicken4WP.Common;
using Chicken4WP.Entities;
using Chicken4WP.Models;
using Chicken4WP.Services.Interface;
using Newtonsoft.Json;

namespace Chicken4WP.Services.Implemention
{
    public class StorageService : IStorageService
    {
        private ChickenDataContext context;

        static StorageService()
        {
            var ctx = new ChickenDataContext();
            if (!ctx.DatabaseExists())
            {
                ctx.CreateDatabase();
                Initialize(ctx);
            }
        }

        private static void Initialize(ChickenDataContext context)
        {
            var proxy = new Setting
            {
                Type = SettingType.ProxySetting,
                IsEnabled = true,
                Name = Const.BASETWEETSERVICENAME,
            };
            context.Settings.InsertOnSubmit(proxy);
            var twip = new Setting
            {
                Type = SettingType.ProxySetting,
                IsEnabled = true,
                IsInUsed = true,
                Name = Const.TWIPTWEETSERVICENAME,
                Data = "https://wxt2005.org/tapi/o/6Z66RF"
            };
            context.Settings.InsertOnSubmit(twip);
            context.SubmitChanges();
        }

        public StorageService()
        {
            this.context = new ChickenDataContext();
        }

        public User GetCurrentUser()
        {
            var data = context.Settings.FirstOrDefault(s => s.Type == SettingType.CurrentUser && s.IsEnabled && s.IsInUsed);
            if (data == null)
                return null;
            var user = JsonConvert.DeserializeObject<User>(data.Data, Const.JsonSettings);
            return user;
        }

        public void UpdateCurrentUser(User user)
        {
            var account = context.Settings.FirstOrDefault(s => s.Type == SettingType.CurrentUser && s.IsEnabled && s.IsInUsed);
            account = new Setting
            {
                Type = SettingType.CurrentUser,
                IsEnabled = true,
                IsInUsed = true,
                Name = user.Name,
                Data = JsonConvert.SerializeObject(user),
            };
            context.SubmitChanges();
        }

        public IList<Setting> GetProxySettings()
        {
            return context.Settings.Where(s => s.Type == SettingType.ProxySetting && s.IsEnabled).ToList();
        }

        public Setting GetCurrentProxySetting()
        {
            return context.Settings.Where(s => s.Type == SettingType.ProxySetting && s.IsEnabled && s.IsInUsed).FirstOrDefault();
        }

        public void UpdageProxySetting(Setting setting)
        {
            var old = context.Settings.SingleOrDefault(s => s.Type == SettingType.ProxySetting && s.IsInUsed);
            if (old != null) old.IsInUsed = false;
            var @new = context.Settings.SingleOrDefault(s => s.Type == setting.Type && s.IsEnabled && s.Name == setting.Name);
            setting.IsInUsed = setting.IsEnabled = true;
            @new = setting;
            context.Settings.InsertOnSubmit(@new);
            context.SubmitChanges();
        }

        public Setting GetCurrentLanguage()
        {
            return context.Settings.FirstOrDefault(s => s.Type == SettingType.LanguageSetting && s.IsInUsed && s.IsEnabled);
        }

        public void UpdateLanguage(string name)
        {
            var setting = context.Settings.FirstOrDefault(s => s.Type == SettingType.LanguageSetting && s.IsEnabled && s.IsInUsed);
            if (setting == null)
            {
                setting = new Setting
                {
                    Type = SettingType.LanguageSetting,
                    Name = name,
                };
                context.Settings.InsertOnSubmit(setting);
            }
            setting.IsEnabled = setting.IsInUsed = true;
            context.SubmitChanges();
        }
    }
}
