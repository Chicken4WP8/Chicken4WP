using System.Linq;
using Chicken4WP.Entities;

namespace Chicken4WP.Services.Implemention
{
    public class TwipTweetService : TweetServiceBase
    {
        public TwipTweetService()
        {
            var twip = ChickenDataContext.Instance.Settings
                .Single(s => s.Type == SettingType.ProxySetting && s.IsEnabled &&
                    s.Name == "Twip4");
            client.BaseUrl = twip.Data;
        }
    }
}
