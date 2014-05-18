using System.Windows;
using Chicken4WP.Services.Interface;

namespace Chicken4WP.Services.Implemention
{
    public class TwipTweetService : TweetServiceBase
    {
        public TwipTweetService()
        {
            var container = (Application.Current.Resources["bootstrapper"] as AppBootstrapper).Container;
            var storageService = container.GetInstance(typeof(IStorageService), null) as IStorageService;
            var twip = storageService.GetCurrentProxySetting();
            client.BaseUrl = twip.Data;
        }
    }
}
