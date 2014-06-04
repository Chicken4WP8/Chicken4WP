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

        string GetCurrentLanguage();
        void UpdateLanguage(string name);

        Tweet GetTempTweet();
        void UpdateTempTweet(Tweet tweet);

        User GetTempUser();
        void UpdateTempUser(User user);

        byte[] GetCachedImage(string url);
        void AddCachedImage(string url, byte[] data);
    }
}
