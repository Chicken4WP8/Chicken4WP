using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;
using Chicken4WP.Models;
using Chicken4WP.Services.Interface;

namespace Chicken4WP.ViewModels.Profile
{
    public class ProfilePageViewModel : Conductor<Screen>.Collection.OneActive
    {
        private UserProfileDetailViewModel detail;
        private UserTweetsViewModel tweets;
        private UserFollowersViewModel followers;
        private UserFollowingViewModel following;
        private UserFavoritesViewModel favorites;
        protected readonly IStorageService storageService;

        public ProfilePageViewModel(
            UserProfileDetailViewModel detail,
            UserTweetsViewModel tweets,
            UserFollowersViewModel followers,
            UserFollowingViewModel following,
            UserFavoritesViewModel favorites,
            IStorageService storageService)
        {
            this.detail = detail;
            this.tweets = tweets;
            this.followers = followers;
            this.following = following;
            this.favorites = favorites;
            this.storageService = storageService;
        }

        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                NotifyOfPropertyChange(() => User);
            }
        }

        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set
            {
                displayName = value;
                NotifyOfPropertyChange(() => DisplayName);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(detail);
            Items.Add(tweets);
            Items.Add(followers);
            Items.Add(following);
            Items.Add(favorites);

            AppBarConductor.Mixin(this);

            User = storageService.GetTempUser();
            DisplayName = "@" + User.ScreenName;
        }
    }
}
