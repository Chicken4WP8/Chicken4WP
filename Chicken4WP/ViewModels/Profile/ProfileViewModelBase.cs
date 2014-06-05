using Chicken4WP.Models;

namespace Chicken4WP.ViewModels.Profile
{
    public abstract class ProfileViewModelBase : PivotItemViewModelBase
    {
        public ProfileViewModelBase()
        {
            this.user = storageService.GetTempUser();
        }

        private User user;
        public User User
        {
            get { return user; }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            if (CheckIsFollowingProtectedUser())
            {
                ProfileInitialize();
            }
        }

        protected override void RefreshData()
        {
            if (CheckIsFollowingProtectedUser())
            {
                ProfileRefreshData();
            }
        }

        protected override void LoadData()
        {
            if (CheckIsFollowingProtectedUser())
            {
                ProfileLoadData();
            }
        }

        protected abstract void ProfileInitialize();
        protected abstract void ProfileRefreshData();
        protected abstract void ProfileLoadData();

        protected bool CheckIsFollowingProtectedUser()
        {
            if (!User.IsFollowing && User.Id != App.CurrentUser.Id)
            {
                toastMessageService.HandleMessage(languageHelper.GetString("Toast_Msg_NotFollowPrivateUser", User.ScreenName));
                base.LoadDataCompleted();
                return false;
            }
            return true;
        }
    }
}
