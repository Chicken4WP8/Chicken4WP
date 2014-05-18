using System.Collections.Generic;
using Chicken4WP.Entities;
using Chicken4WP.Models;

namespace Chicken4WP.Services.Interface
{
    public interface IStorageService
    {
        User GetCurrentUser();
        void UpdateCurrentUser(User user);

        IList<Setting> GetProxySettings();
        Setting GetCurrentProxySetting();
        void UpdageProxySetting(Setting setting);

        Setting GetCurrentLanguage();
        void UpdateLanguage(string name);
    }
}
